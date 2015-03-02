﻿using System;
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

namespace BLL
{
    public class FileBLL
    {

        static string filterString = "_avc_hd,_avc,_hd,480p,720p,1080p,2160p,_,480,720,1080,[,],.,2000,4000,8000,12000,6000,1500, ,540,qhd,fullhd,-,high,low";
        Dictionary<string, HisTorrent> dic;
        Dictionary<string, HisTorrent> fileDic;
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
            getList();
            startTime = DateTime.Now;
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {
                if (p.EndsWith(".torrent"))
                {
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
                                    if(ifCheckHis)
                                        DBHelper.insertTorrent(his);
                                    try
                                    {
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

    }
}
