using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lite.View
{
    public class Country
    {
        private string name_Eng;
        private string name_Kor;
        private string code;
        private string lang_Code;

        public string Name_Kor { get => name_Kor; set => name_Kor = value; }
        public string Name_Eng { get => name_Eng; set => name_Eng = value; }
        public string Code { get => code; set => code = value; }
        public string Lang_Code { get => lang_Code; set => lang_Code = value; }




    }
}
