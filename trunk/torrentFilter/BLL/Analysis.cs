using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;

namespace GetSize
{
    public class Analysis
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        ScriptEngine engine;
        Regex r1;
        Regex r2;
        public Analysis()
        {
            initMap();
            r1 = new Regex("<img src=\".*?px;\"/>"); 
            r2=new Regex("Vídeo Id: .*?<br/>");
            engine = new ScriptEngine();
            engine.Evaluate(@"var _0xdd02 = ['host', 'javjunkies.com', 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=', '', 'substr', 'charAt', 'indexOf', 'fromCharCode', 'length', 'join', 'charCodeAt'];
function jj(_0x95d8x2) {
	var _0x95d8x3 = _0xdd02[2];
	var _0x95d8x4, _0x95d8x5, _0x95d8x6, _0x95d8x7, _0x95d8x8, _0x95d8x9, _0x95d8xa, _0x95d8xb, _0x95d8xc = 0, _0x95d8xd = 0, _0x95d8xe = _0xdd02[3], _0x95d8xf = [];
	if (!_0x95d8x2) {
		return _0x95d8x2
	}
	;
	_0x95d8x2 = _0x95d8x2[_0xdd02[4]](3);
	_0x95d8x2 += _0xdd02[3];
	do {
		_0x95d8x7 = _0x95d8x3[_0xdd02[6]](_0x95d8x2[_0xdd02[5]](_0x95d8xc++));
		_0x95d8x8 = _0x95d8x3[_0xdd02[6]](_0x95d8x2[_0xdd02[5]](_0x95d8xc++));
		_0x95d8x9 = _0x95d8x3[_0xdd02[6]](_0x95d8x2[_0xdd02[5]](_0x95d8xc++));
		_0x95d8xa = _0x95d8x3[_0xdd02[6]](_0x95d8x2[_0xdd02[5]](_0x95d8xc++));
		_0x95d8xb = _0x95d8x7 << 18 | _0x95d8x8 << 12 | _0x95d8x9 << 6
				| _0x95d8xa;
		_0x95d8x4 = _0x95d8xb >> 16 & 0xff;
		_0x95d8x5 = _0x95d8xb >> 8 & 0xff;
		_0x95d8x6 = _0x95d8xb & 0xff;
		if (_0x95d8x9 == 64) {
			_0x95d8xf[_0x95d8xd++] = String[_0xdd02[7]](_0x95d8x4)
		} else {
			if (_0x95d8xa == 64) {
				_0x95d8xf[_0x95d8xd++] = String[_0xdd02[7]](_0x95d8x4,
						_0x95d8x5)
			} else {
				_0x95d8xf[_0x95d8xd++] = String[_0xdd02[7]](_0x95d8x4,
						_0x95d8x5, _0x95d8x6)
			}
		}
	} while (_0x95d8xc < _0x95d8x2[_0xdd02[8]]);
	_0x95d8xe = _0x95d8xf[_0xdd02[9]](_0xdd02[3]);
	return utf8d(_0x95d8xe)
}

function utf8d(_0x95d8x11) {
	var _0x95d8x12 = _0xdd02[3], _0x95d8xc = 0, _0x95d8x13 = c1 = c2 = 0;
	while (_0x95d8xc < _0x95d8x11[_0xdd02[8]]) {
		_0x95d8x13 = _0x95d8x11[_0xdd02[10]](_0x95d8xc);
		if (_0x95d8x13 < 128) {
			_0x95d8x12 += String[_0xdd02[7]](_0x95d8x13);
			_0x95d8xc++
		} else {
			if ((_0x95d8x13 > 191) && (_0x95d8x13 < 224)) {
				c2 = _0x95d8x11[_0xdd02[10]](_0x95d8xc + 1);
				_0x95d8x12 += String[_0xdd02[7]](((_0x95d8x13 & 31) << 6)
						| (c2 & 63));
				_0x95d8xc += 2
			} else {
				c2 = _0x95d8x11[_0xdd02[10]](_0x95d8xc + 1);
				c3 = _0x95d8x11[_0xdd02[10]](_0x95d8xc + 2);
				_0x95d8x12 += String[_0xdd02[7]](((_0x95d8x13 & 15) << 12)
						| ((c2 & 63) << 6) | (c3 & 63));
				_0x95d8xc += 3
			}
		}
	}
	;
	return _0x95d8x12
}");

        }

        public ArrayList alys(string content)
        {
            string partTorrentUrl = findKey(content);
            ArrayList list = new ArrayList();
            if (content.Contains("Sorry, but you are looking for something that isn't here."))
            {
                Console.WriteLine( " NOT FOUND");
                return list;
            }
            string[] content1 = content.Split(new string[] { "<div class=\"image\">" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < content1.Length; i++)
            {
                if (content1[i].Contains("OLink('&#") || content1[i].Contains("OLink(&#"))
                {
                    His his = new His();
                    his.OriginalHtml = content1[i];
                    string s= content1[i].Split(new string[] { "<script>document.write(jj(\"", "\"))</script>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    string res = engine.CallGlobalFunction<string>("jj", s);
                    res = res.Replace(">", "/>");

                    MatchCollection mc = r1.Matches(res);
                    foreach (Match m in mc)
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(m.ToString());
                        string key = xmlDoc.GetElementsByTagName("img")[0].Attributes["style"].Value;
                        key = key.Substring(0, key.IndexOf(";"));
                        string cha = dictionary[key];
                        res = res.Replace(m.ToString(), cha);
                    }

                    his.Info = res;
                    try
                    {
                        try
                        {
                            his.Vid = r2.Matches(res)[0].ToString().Split(new string[] { "Vídeo Id: ", "<br/>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("VID NOT FOUND!!");
                            Console.WriteLine(e.Message);
                        }
                        string size = res.Split(new string[] { "<br/>Sìze: ", "<br/><u/>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                        if (size.EndsWith("GB"))
                        {
                            size = size.Replace("Size:", "").Replace("GB", "");
                            his.Size = Convert.ToDouble(size) * 1024;

                        }
                        else
                            his.Size = Convert.ToDouble(size.Replace("MB", "").Replace("KB", ""));
                        his.Actress = res.Split(new string[] { "<br/>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        his.FileCount =Convert.ToInt32( res.Split(new string[] { "Fíles in tørrent:</u/>", "fìles<br/>" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                        try
                        {
                            his.Files = res.Split(new string[] { "fìles<br/>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                        catch
                        {
                            Console.WriteLine("NO FILE LIST");
                        }
                        his.Html=getImg(his,partTorrentUrl);

                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine(res);
                    }
                    list.Add(his);
                }
            }
            return list;
        }

        public string findKey(string s)
        {
            string key = "";
            try
            {
                key = s.Substring(s.IndexOf("http://javjunkies.com/main/JavJ.php?k="), "http://javjunkies.com/main/JavJ.php?k=".Length + 4);
            }
            catch
            {
                Console.WriteLine("CAN NOT FIND KEY");
                key = "00000";
            }
            return key;
        }

        private string getImg(His his,string url)
        {
            Regex r2 = new Regex("file=[a-z0-9]*");
            string torrent = r2.Matches(his.OriginalHtml)[0].Value;
            string res = "";
            Regex r = new Regex("http://.*?\\)");
            string img = r.Matches(his.OriginalHtml)[0].Value.Replace("'", "").Replace(")", "").Replace("&#39;", "");
            res = "<a href=\"" + url + "&" + torrent + "\"><img src=\"" + img + "\"></a><br>\r\n" + his.Info + "\r\n";
            return res;
        }


        public void initMap()
        {
            if (dictionary.Count == 0)
            {
                dictionary.Add("background-position:0", "A");
                dictionary.Add("background-position:-13px", "B");
                dictionary.Add("background-position:-26px", "C");
                dictionary.Add("background-position:-38px", "D");
                dictionary.Add("background-position:-51px", "E");
                dictionary.Add("background-position:-63px", "F");
                dictionary.Add("background-position:-73px", "G");
                dictionary.Add("background-position:-88px", "H");
                dictionary.Add("background-position:-100px", "I");
                dictionary.Add("background-position:-106px", "J");
                dictionary.Add("background-position:-116px", "K");
                dictionary.Add("background-position:-129px", "L");
                dictionary.Add("background-position:-140px", "M");
                dictionary.Add("background-position:-154px", "N");
                dictionary.Add("background-position:-167px", "O");
                dictionary.Add("background-position:-181px", "P");
                dictionary.Add("background-position:-192px", "Q");
                dictionary.Add("background-position:-207px", "R");
                dictionary.Add("background-position:-219px", "S");
                dictionary.Add("background-position:-231px", "T");
                dictionary.Add("background-position:-242px", "U");
                dictionary.Add("background-position:-254px", "V");
                dictionary.Add("background-position:-266px", "W");
                dictionary.Add("background-position:-283px", "X");
                dictionary.Add("background-position:-294px", "Y");
                dictionary.Add("background-position:-306px", "Z");
                dictionary.Add("background-position:-328px", "1");
                dictionary.Add("background-position:-336px", "2");
                dictionary.Add("background-position:-348px", "3");
                dictionary.Add("background-position:-358px", "4");
                dictionary.Add("background-position:-368px", "5");
                dictionary.Add("background-position:-378px", "6");
                dictionary.Add("background-position:-389px", "7");
                dictionary.Add("background-position:-398px", "8");
                dictionary.Add("background-position:-408px", "9");
                dictionary.Add("background-position:-317px", "0");
            }

        }

    }
}
