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
           
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.AllDirectories);
            foreach (String p in path)
            {
                if (p.EndsWith(".torrent"))
                {
                    BDict torrentFile = BencodingUtils.DecodeFile(p) as BDict;
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
                                BList list = (BList)bd["path"];
                                string s = ((BString)list[list.Count - 1]).Value;
                                HisTorrent trt = new HisTorrent();
                                trt.CreateTime = DateTime.Now;
                                trt.Path = p;
                                trt.Size = length;
                                trt.File = s.Substring(0, s.LastIndexOf('.'));
                                trt.Ext = s.Substring(s.LastIndexOf('.'));
                                listTorrent.Add(trt);
                                if (DBHelper.checkTorrent(trt) > 0 && trt.Size > 10 * 1024 * 1024)
                                {
                                    flag = false;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            HisTorrent trt = new HisTorrent();
                            trt.CreateTime = DateTime.Now;
                            trt.Path = p;
                            trt.Size = ((BInt)(torrentFile["info"] as BDict)["length"]).Value;
                            string name = ((BString)(torrentFile["info"] as BDict)["name"]).Value;
                            trt.File = name.Substring(0, name.LastIndexOf('.'));
                            trt.Ext = name.Substring(name.LastIndexOf('.'));
                            listTorrent.Add(trt);
                            if (DBHelper.checkTorrent(trt) > 0 && trt.Size > 10 * 1024 * 1024)
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            foreach (HisTorrent his in listTorrent)
                            {
                                DBHelper.insertTorrent(his);
                            }
                        }
                        else
                        {
                            string dupPath = Path.Combine(Path.GetDirectoryName(p), "duplicate");
                            if (!Directory.Exists(dupPath))
                            {
                                Directory.CreateDirectory(dupPath);
                            }
                            File.Move(p, Path.Combine(dupPath, Path.GetFileName(p)));
                        }
                    }
                    else
                        Console.WriteLine("decode Error  " + p);
                }

            }




        }

       
    }
}

