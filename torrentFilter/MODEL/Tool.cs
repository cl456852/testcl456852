using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class Tool
    {
        public static string filterString = "_avc_hd,_avc,_hd,480p,720p,1080p,1440p,2160p,_,480,720,1080,2160,[,],.,2000,4000,8000,12000,6000,1500, ,540,qhd,fullhd,-,high,low,sd,$$,rarbg,com,ktr,xxx,%22,%20,YAPG,PROPER,6500,4500,3000,1200";
        public static string filterName(string fileName)
        {
            fileName = fileName.ToLower();
            string[] strs = Tool.filterString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strs)
            {
                fileName = fileName.Replace(s.ToLower(), "");
            }
            return fileName;
        }
    }
}
