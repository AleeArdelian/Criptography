using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belaso_Vigenere_Cipher.Model
{
    public class UiElements : BindableBase
    {
        private string keyTextbox;
        private string messageTextbox;
        private string encriptedTextbox;
        private string decryptedTextbox;
        private Dictionary<char, int> alphabetKeys;
        private Dictionary<int, char> integerKeys;


        public string KeyTextBox
        {
            get { return this.keyTextbox; }
            set { SetProperty(ref keyTextbox, value); }
        }

        public string MessageTextBox
        {
            get { return messageTextbox; }
            set { SetProperty(ref messageTextbox, value); }
        }

        public string EncriptedTextBox
        {
            get { return encriptedTextbox; }
            set { SetProperty(ref encriptedTextbox, value); }

        }
        public string DecryptedTextBox
        {
            get { return decryptedTextbox; }
            set { SetProperty(ref decryptedTextbox, value); }
        }

        public Dictionary<char,int> AplhabetKeys
        { get { return alphabetKeys; }  }

        public Dictionary<int, char> IntegerKeys
        {
            get { return integerKeys; }
        }        

        public UiElements()
        {
            alphabetKeys = new Dictionary<char, int>()
            {{' ',0 },{'a',1 },{'b',2 },{'c',3 },{'d',4 },{'e',5 },{'f',6 },{'g',7 },{'h',8 },{'i',9 },{'j',10 }
            ,{'k',11 },{'l',12 },{'m',13 },{'n',14 },{'o',15 },{'p',16 },{'q',17 },{'r',18 },{'s',19 },{'t',20 }
            ,{'u',21 },{'v',22 },{'w',23 },{'x',24 },{'y',25 },{'z',26 } };

            integerKeys = alphabetKeys.ToDictionary(x => x.Value, x => x.Key);
        }

    }
}
