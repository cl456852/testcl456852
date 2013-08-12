using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using DAL;
using System.IO;
using MODEL;
using System.Reflection;
using BencodeLibrary;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.BencodeTest();
            Console.Read();
        }

        void jsTest()
        {
            var engine = new Jurassic.ScriptEngine();
//            Console.WriteLine(engine.Evaluate(@"eval(function(p, a, c, k, e, d) {
//	e = function(c) {
//		return (c < a ? '' : e(parseInt(c / a)))
//				+ ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c
//						.toString(36))
//	};
//	if (!''.replace(/^/, String)) {
//		while (c--) {
//			d[e(c)] = k[c] || e(c)
//		}
//		k = [ function(e) {
//			return d[e]
//		} ];
//		e = function() {
//			return '\\w+'
//		};
//		c = 1
//	}
//	;
//	while (c--) {
//		if (k[c]) {
//			p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c])
//		}
//	}
//	return p
//}
//		(
//				'E a=[\"\\l\\e\\t\\j\",\"\\v\\u\\V\\v\\Q\\r\\U\\y\\i\\t\\1s\\A\\e\\K\",\"\\I\\1t\\D\\1r\\1q\\1n\\1o\\1p\\1u\\1v\\1A\\1B\\1z\\1y\\T\\1w\\1x\\1m\\1l\\1b\\1d\\1a\\17\\19\\1c\\1e\\u\\Z\\A\\z\\i\\L\\R\\l\\y\\v\\U\\Y\\K\\r\\e\\1j\\1k\\f\\t\\j\\Q\\V\\1i\\X\\1h\\1f\\1g\\1C\\1H\\1D\\1Q\\1P\\1T\\1S\\1U\\1V\\1O\\1M\\1G\",\"\",\"\\t\\Q\\Z\\t\\j\\f\",\"\\A\\l\\u\\f\\I\\j\",\"\\y\\r\\z\\i\\X\\T\\L\",\"\\L\\f\\e\\K\\D\\l\\u\\f\\D\\e\\z\\i\",\"\\Y\\i\\r\\R\\j\\l\",\"\\v\\e\\y\\r\",\"\\A\\l\\u\\f\\D\\e\\z\\i\\I\\j\"];W 1J(c){k(1R[a[0]]!==a[1]){F a[1]};E o=a[2];E m,w,P,H,O,B,C,n,b=0,x=0,N=a[3],q=[];k(!c){F c};c=c[a[4]](3);c+=a[3];1L{H=o[a[6]](c[a[5]](b++));O=o[a[6]](c[a[5]](b++));B=o[a[6]](c[a[5]](b++));C=o[a[6]](c[a[5]](b++));n=H<<18|O<<12|B<<6|C;m=n>>16&J;w=n>>8&J;P=n&J;k(B==13){q[x++]=g[a[7]](m)}G{k(C==13){q[x++]=g[a[7]](m,w)}G{q[x++]=g[a[7]](m,w,P)}}}14(b<c[a[8]]);N=q[a[9]](a[3]);F 11(N)};W 11(h){E p=a[3],b=0,d=1F=s=0;14(b<h[a[8]]){d=h[a[10]](b);k(d<1E){p+=g[a[7]](d);b++}G{k((d>1N)&&(d<1I)){s=h[a[10]](b+1);p+=g[a[7]](((d&1K)<<6)|(s&M));b+=2}G{s=h[a[10]](b+1);S=h[a[10]](b+2);p+=g[a[7]](((d&15)<<12)|((s&M)<<6)|(S&M));b+=3}}};F p};',
//                62,
//                120,
//                '||||||||||_0xdd02|_0x95d8xc|_0x95d8x2|_0x95d8x13|x6F|x72|String|_0x95d8x11|x65|x74|if|x68|_0x95d8x4|_0x95d8xb|_0x95d8x3|_0x95d8x12|_0x95d8xf|x6E|c2|x73|x61|x6A|_0x95d8x5|_0x95d8xd|x69|x64|x63|_0x95d8x9|_0x95d8xa|x43|var|return|else|_0x95d8x7|x41|0xff|x6D|x66|63|_0x95d8xe|_0x95d8x8|_0x95d8x6|x75|x67|c3|x4F|x6B|x76|function|x78|x6C|x62||utf8d||64|while|||x57||x58|x56|x54|x59|x55|x5A|x7A|x30|x79|x77|x70|x71|x53|x52|x46|x47|x48|x45|x44|x2E|x42|x49|x4A|x50|x51|x4E|x4D|x4B|x4C|x31|x33|128|c1|x3D|x32|224|jj|31|do|x2F|191|x2B|x35|x34|location|x37|x36|x38|x39'
//                        .split('|'), 0, {}))
//"));
        }

        void BencodeTest()
        {
            BDict torrentFile = BencodingUtils.DecodeFile(@"E:\Program Files (x86)\BitSpirit\Torrent - 副本\51C2F0FB04B2EFD2.torrent") as BDict;

        }

    }
}
