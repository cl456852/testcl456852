using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Linq;
using System.IO;
using System.Collections;
using MODEL;
using System.Text.RegularExpressions;
using GetSize;
using BencodeLibrary;
using DB;
using System.Security.Cryptography;

namespace BLL
{
    public class FileBLL
    {
        MD5 md5 = MD5.Create();
        static string filterString = "_avc_hd,_avc,_hd,480p,720p,1080p,1440p,2160p,_,480,720,1080,[,],.,2000,4000,8000,12000,6000,1500, ,540,qhd,fullhd,-,high,low,SD";
        Dictionary<string, HisTorrent> dic;
        Dictionary<string, HisTorrent> fileDic;
        HashSet<String> md5Set = new HashSet<string>();
        DateTime startTime;
        public FileBLL()
        {
     
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }

        public void process(string directoryStr,bool ifCheckHis)
        {
            string md5;
            getList();
            startTime = DateTime.Now;
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {
                if (p.EndsWith(".torrent"))
                {
                    md5 = GetMd5(p);
                    if (md5Set.Contains(md5))
                    {
                        moveFile("Md5Duplicate",p);
                        continue;
                    }
                    BDict torrentFile = null;
                    bool hasBigFile = false;
                    try
                    {
                        torrentFile = BencodingUtils.DecodeFile(p) as BDict;
                    }
                    catch (Exception e)
                    {
                        moveFile("decodeErr", p);
                    }
                    if (torrentFile != null)
                    {
                        bool flag = true;
                        BList b;
                        List<HisTorrent> listTorrent = new List<HisTorrent>();
                        if ((torrentFile["info"] as BDict).ContainsKey("files"))
                        {

                            b = (BList)(torrentFile["info"] as BDict)["files"];



                            for (int i = 0; i < b.Count; i++)
                            {
                                BDict bd = (BDict)b[i];
                                long length = ((BInt)bd["length"]).Value;
                                if (length > 25 * 1024 * 1024)
                                    hasBigFile = true;
                                BList list = (BList)bd["path"];
                                string s = ((BString)list[list.Count - 1]).Value;
                                HisTorrent trt = new HisTorrent();
                                trt.CreateTime = DateTime.Now;
                                trt.Path = p;
                                trt.Size = length;
                                if (s.LastIndexOf('.') > 0)
                                {
                                    trt.File = s.Substring(0, s.LastIndexOf('.'));
                                    trt.Ext = s.Substring(s.LastIndexOf('.'));
                                }
                                else
                                {
                                    trt.File = s;
                                }
                                listTorrent.Add(trt);
                                if (!check(trt,ifCheckHis))
                                {
                                    flag = false;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            hasBigFile = true;
                            HisTorrent trt = new HisTorrent();
                            trt.CreateTime = DateTime.Now;
                            trt.Path = p;
                            trt.Size = ((BInt)(torrentFile["info"] as BDict)["length"]).Value;
                            string name = ((BString)(torrentFile["info"] as BDict)["name"]).Value;
                            if (name.LastIndexOf('.') > 0)
                            {
                                trt.File = name.Substring(0, name.LastIndexOf('.'));
                                trt.Ext = name.Substring(name.LastIndexOf('.'));
                            }
                            else
                            {
                                trt.File = name;
                            }
                            listTorrent.Add(trt);
                            if (!check(trt,ifCheckHis))
                            {
                                flag = false;
                            }
                        }
                        if (!hasBigFile)
                        {
                            moveFile("noBigFile", p);
                        }
                        if (flag && hasBigFile)
                        {
                            foreach (HisTorrent his in listTorrent)
                            {
                                if (his.Size > 60 * 1024 * 1024)
                                {
                                    if (ifCheckHis)
                                    {
                                        his.Md5 = md5;
                                        DBHelper.insertTorrent(his);
                                    }
                                    try
                                    {
                                        md5Set.Add(md5);
                                        dic.Add(filterName(his.File), his);
                                    }catch(ArgumentException e)
                                    {
                                        dic.Remove(filterName(his.File));
                                        dic.Add(filterName(his.File), his);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        moveFile("decodeErr", p);
                    }
                }

            }




        }

        bool check(HisTorrent trt, bool ifCheckHis)
        {
            bool flag = true;
            if (trt.Size > 60 * 1024 * 1024)
            {
                if (fileDic.ContainsKey(filterName( trt.File)) )
                {
                    moveFile("duplicate", trt.Path);
                    return false;
                }
                if (ifCheckHis)
                {
                    HisTorrent t;
                    try
                    {
                        t = dic[filterName(trt.File)];

                    }
                    catch (KeyNotFoundException e)
                    {
                        t = null;
                    }
                    if (t != null)
                    {

                        if (t.Size >= trt.Size || t.CreateTime < startTime)
                        {
                            moveFile("duplicate", trt.Path);
                            flag = false;
                        }
                        else
                        {
                            moveFile("duplicate", t.Path);
                        }
                    }
                    return flag;
                }
                else
                    return true;
            }
            else
                return true;
        }


        void moveFile(string folderName, string path)
        {
            if (File.Exists(path))
            {
                string targetDir = Path.Combine(Path.GetDirectoryName(path), folderName);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                try
                {
                    File.Move(path, Path.Combine(targetDir, Path.GetFileName(path)));
                    if (File.Exists(path + ".htm"))
                    {
                        File.Move(path+".htm", Path.Combine(targetDir, Path.GetFileName(path))+".htm");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("path too long    " + path);
                    File.Move(path, Path.Combine(targetDir, Path.GetFileName(path)).Substring(0, 240) + ".torrent");
                    File.Move(path+".htm", Path.Combine(targetDir, Path.GetFileName(path)).Substring(0, 240) + ".htm");
                    Console.WriteLine("path too long    " + Path.Combine(targetDir, Path.GetFileName(path)).Substring(0, 240) + ".torrent");
                }
                Console.WriteLine(folderName + " " + path);
            }

        }

        void getList()
        {
            dic = DBHelper.getList(filterString);
            fileDic = DBHelper.getFileList(filterString);
            foreach (HisTorrent his in fileDic.Values)
            {
                if(his.Md5!=null)
                    md5Set.Add(his.Md5);
            }
            //foreach (string s in dic.Keys)
            //{
            //    Console.WriteLine(s);
            //}
        }

        string filterName(string fileName)
        {
            fileName = fileName.ToLower();
            string[] strs = filterString.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strs)
            {
                fileName = fileName.Replace(s, "");
            }
            return fileName;
        }

        public static string GetMd5(string pathName)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;

            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                oFileStream = new System.IO.FileStream(pathName.Replace("\"", ""), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值

                oFileStream.Close();

                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”

                strHashData = System.BitConverter.ToString(arrbytHashValue);

                //替换-
                strHashData = strHashData.Replace("-", "");

                strResult = strHashData;
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strResult;
        }

    }
}

