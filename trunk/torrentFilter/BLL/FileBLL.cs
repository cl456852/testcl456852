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

namespace BLL
{
    public class FileBLL
    {

    //    Analysis ana;
        //_18javAnaysis ana;
       // HelloJavAnalysis ana;
        _141javAnalysis ana;
        Filter filter;


        DateTime startTime;
        public FileBLL()
        {
            filter = new Filter();
           // ana = new Analysis();
            ana = new _141javAnalysis();
          //  ana = new _18javAnaysis();
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }

        public void process(string directoryStr)
        {
            startTime = new DateTime();
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {
                if (p.EndsWith(".torrent"))
                {
                    BDict torrentFile=null;
                    bool hasBigFile = false;
                    try
                    {
                        torrentFile = BencodingUtils.DecodeFile(p) as BDict;
                    }
                    catch(Exception e)
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
                                if (trt.Size > 100 * 1024 * 1024 && (DBHelper.checkTorrent(trt) > 0 || DBHelper.checkFiles(trt) > 0))
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
                                trt.Ext =name.Substring(name.LastIndexOf('.'));
                            }
                            else
                            {
                                trt.File = name;
                            }
                            listTorrent.Add(trt);
                            if (trt.Size > 100 * 1024 * 1024 && (DBHelper.checkTorrent(trt) > 0 || DBHelper.checkFiles(trt) > 0))
                            {
                                flag = false;
                            }
                        }
                        if (flag && hasBigFile)
                        {
                            foreach (HisTorrent his in listTorrent)
                            {
                                DBHelper.insertTorrent(his);
                            }
                        }
                        else
                        {
                            if (!hasBigFile)
                            {
                                moveFile("noBigFile", p);
                            }
                            else
                            {
                                moveFile("duplicate", p);
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

        void check(HisTorrent trt)
        {
            HisTorrent temp;
            HisTorrent[] res = DBHelper.checkFiles1(trt);
            if (res != null)
            {
                foreach(HisTorrent t in res)
                {
                if (t.Size > trt.Size||t.CreateTime<startTime)
                {
                    moveFile("duplicate", trt.Path);
                }
                else
                {
                    moveFile("duplicate", t.Path);
                }
                }
            }
        }


        void moveFile(string folderName,string path)
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
                }
                catch (Exception e)
                {
                    Console.WriteLine("path too long    " + path);
                    File.Move(path, Path.Combine(targetDir, Path.GetFileName(path)).Substring(0,240)+".torrent");
                    Console.WriteLine("path too long    "+Path.Combine(targetDir, Path.GetFileName(path)).Substring(0, 240) + ".torrent");
                }
                Console.WriteLine(folderName + " " + path);
            }

        }
       
    }
}

