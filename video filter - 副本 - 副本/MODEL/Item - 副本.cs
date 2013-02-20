using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MODEL
{
    public class Item
    {
        public static string key;
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public static string img;
        public static string torrent;
        public static string info;
        public static string id;
        public static string size;

        public static string analysis(string s)
        {

            var engine = new Jurassic.ScriptEngine();
            engine.Evaluate(@"var _0x37e6 = ['\x41\x42\x43\x44\x45\x46\x47\x48\x49\x4A\x4B\x4C\x4D\x4E\x4F\x50\x51\x52\x53\x54\x55\x56\x57\x58\x59\x5A\x61\x62\x63\x64\x65\x66\x67\x68\x69\x6A\x6B\x6C\x6D\x6E\x6F\x70\x71\x72\x73\x74\x75\x76\x77\x78\x79\x7A\x30\x31\x32\x33\x34\x35\x36\x37\x38\x39\x2B\x2F\x3D', '', '\x73\x75\x62\x73\x74\x72', '\x63\x68\x61\x72\x41\x74', '\x69\x6E\x64\x65\x78\x4F\x66', '\x66\x72\x6F\x6D\x43\x68\x61\x72\x43\x6F\x64\x65', '\x6C\x65\x6E\x67\x74\x68', '\x6A\x6F\x69\x6E', '\x63\x68\x61\x72\x43\x6F\x64\x65\x41\x74'];
function jj(_0x79b1x2) {
    var _0x79b1x3 = _0x37e6[0];
    var _0x79b1x4, _0x79b1x5, _0x79b1x6, _0x79b1x7, _0x79b1x8, _0x79b1x9, _0x79b1xa, _0x79b1xb, _0x79b1xc = 0,
    _0x79b1xd = 0,
    _0x79b1xe = _0x37e6[1],
    _0x79b1xf = [];
    if (!_0x79b1x2) {
        return _0x79b1x2;
    };
    _0x79b1x2 = _0x79b1x2[_0x37e6[2]](2);
    _0x79b1x2 += _0x37e6[1];
    do {
        _0x79b1x7 = _0x79b1x3[_0x37e6[4]](_0x79b1x2[_0x37e6[3]](_0x79b1xc++));
        _0x79b1x8 = _0x79b1x3[_0x37e6[4]](_0x79b1x2[_0x37e6[3]](_0x79b1xc++));
        _0x79b1x9 = _0x79b1x3[_0x37e6[4]](_0x79b1x2[_0x37e6[3]](_0x79b1xc++));
        _0x79b1xa = _0x79b1x3[_0x37e6[4]](_0x79b1x2[_0x37e6[3]](_0x79b1xc++));
        _0x79b1xb = _0x79b1x7 << 18 | _0x79b1x8 << 12 | _0x79b1x9 << 6 | _0x79b1xa;
        _0x79b1x4 = _0x79b1xb >> 16 & 0xff;
        _0x79b1x5 = _0x79b1xb >> 8 & 0xff;
        _0x79b1x6 = _0x79b1xb & 0xff;
        if (_0x79b1x9 == 64) {
            _0x79b1xf[_0x79b1xd++] = String[_0x37e6[5]](_0x79b1x4);
        } else {
            if (_0x79b1xa == 64) {
                _0x79b1xf[_0x79b1xd++] = String[_0x37e6[5]](_0x79b1x4, _0x79b1x5);
            } else {
                _0x79b1xf[_0x79b1xd++] = String[_0x37e6[5]](_0x79b1x4, _0x79b1x5, _0x79b1x6);
            };
        };
    } while ( _0x79b1xc < _0x79b1x2 [ _0x37e6 [ 6 ]]);;
    _0x79b1xe = _0x79b1xf[_0x37e6[7]](_0x37e6[1]);
    return utf8d(_0x79b1xe);
};
function utf8d(_0x79b1x11) {
    var _0x79b1x12 = _0x37e6[1],
    _0x79b1xc = 0,
    _0x79b1x13 = c1 = c2 = 0;
    while (_0x79b1xc < _0x79b1x11[_0x37e6[6]]) {
        _0x79b1x13 = _0x79b1x11[_0x37e6[8]](_0x79b1xc);
        if (_0x79b1x13 < 128) {
            _0x79b1x12 += String[_0x37e6[5]](_0x79b1x13);
            _0x79b1xc++;
        } else {
            if ((_0x79b1x13 > 191) && (_0x79b1x13 < 224)) {
                c2 = _0x79b1x11[_0x37e6[8]](_0x79b1xc + 1);
                _0x79b1x12 += String[_0x37e6[5]](((_0x79b1x13 & 31) << 6) | (c2 & 63));
                _0x79b1xc += 2;
            } else {
                c2 = _0x79b1x11[_0x37e6[8]](_0x79b1xc + 1);
                c3 = _0x79b1x11[_0x37e6[8]](_0x79b1xc + 2);
                _0x79b1x12 += String[_0x37e6[5]](((_0x79b1x13 & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                _0x79b1xc += 3;
            };
        };
    };
    return _0x79b1x12;
};");
            string res = engine.CallGlobalFunction<string>("jj", s);
            //Regex r2 = new Regex(@"Vídeo Id: .*<br>Sìze:");
            //Item.id = r2.Matches(res)[0].Value.Split(new string[] { "Vídeo Id: ", "<br>Sìze:" }, StringSplitOptions.RemoveEmptyEntries)[0];
            //Item.id = "<a>" + Item.torrent.Replace(">", "/>")+"</a>";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(Item.id);
            //string id="";
            //foreach (XmlNode x in xmlDoc.GetElementsByTagName("a")[0].ChildNodes)
            //{
            //    string key = x.Attributes["style"].Value;
            //    key = key.Substring(0, key.IndexOf(";") + 1);
            //    key = dictionary[key];
            //    id += key;
            //}
            res = res.Replace(">", "/>");
            Regex r1 = new Regex("<img src=\".*?px;\"/>");

            MatchCollection mc = r1.Matches(res);
            foreach(Match m in mc)
            {
 
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(m.ToString());
                string key = xmlDoc.GetElementsByTagName("img")[0].Attributes["style"].Value;
                key = key.Substring(0, key.IndexOf(";"));
                string cha = dictionary[key];
                res= res.Replace(m.ToString(), cha);
            }
            Item.info = res;
            Regex r2 = new Regex("Vídeo Id: .*?<br/>");
            Item.id = r2.Matches(res)[0].ToString().Split(new string[] { "Vídeo Id: ", "<br/>" },StringSplitOptions.RemoveEmptyEntries)[0];
            return Item.id;

        }
    }
}
