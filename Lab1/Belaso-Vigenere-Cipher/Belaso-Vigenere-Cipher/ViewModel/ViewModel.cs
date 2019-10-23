using Belaso_Vigenere_Cipher.Model;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using System.Windows;
using System;

namespace Belaso_Vigenere_Cipher.ViewModel
{
    public class ViewModel
    {
        public UiElements UiElements { get; set; }
        public ICommand EncryptCommand { get; private set; }
        public ICommand DecryptCommand { get; private set; }

        public ViewModel()
        {
            UiElements = new UiElements();
            EncryptCommand = new DelegateCommand(Encript);
            DecryptCommand = new DelegateCommand(Decript);
        }

        public List<int> TransformKeyIntoNumerical()
        {
            List<int> _numericalKey = new List<int>();
            if (String.IsNullOrEmpty(UiElements.KeyTextBox))
            {
                MessageBox.Show("Please insert a key!");
                return null;
            }
            else
            {
                char[] _keyLetters = UiElements.KeyTextBox.ToCharArray();
                foreach (char x in _keyLetters)
                {
                    _numericalKey.Add(UiElements.AplhabetKeys[x]);
                }
                return _numericalKey;
            }
        }   

        public List<int> PlaintextEncryption()
        {
            List<int> _numericalKey = TransformKeyIntoNumerical();
            if (_numericalKey == null) return null;
            if (String.IsNullOrEmpty(UiElements.MessageTextBox))
            {
                MessageBox.Show("Please insert a message!");
                return null;
            }
            char[] _plaintextLetters = UiElements.MessageTextBox.ToCharArray();

            List<int> _encryptedPlaintext = new List<int>();

            int k=0;
            for( int i =0; i< _plaintextLetters.Length; i++)
            {  
                if (i % _numericalKey.Count == 0)
                    k = 0;

                var x = ( UiElements.AplhabetKeys[_plaintextLetters[i]] + _numericalKey[k])% UiElements.AplhabetKeys.Count;
                _encryptedPlaintext.Add(x);
                k++;
            }
            return _encryptedPlaintext;
        }

        public List<int> CiphertextToEncryption()
        {
            List<int> _numericalKey = TransformKeyIntoNumerical();
            if (_numericalKey == null) return null;
            if (String.IsNullOrEmpty(UiElements.EncriptedTextBox))
            {
                MessageBox.Show("Please insert an encrypted message!");
                return null;
            }
            char[] _ciphertextLetters = UiElements.EncriptedTextBox.ToLower().ToCharArray();

            List<int> _encryptedCiphertext = new List<int>();

            int k = 0;
            for (int i = 0; i < _ciphertextLetters.Length; i++)
            {
                if (i % _numericalKey.Count == 0)
                    k = 0;

                var x = UiElements.AplhabetKeys[_ciphertextLetters[i]] - _numericalKey[k];
                if (x>=0 )
                    x %= UiElements.AplhabetKeys.Count;
                else
                    x+= UiElements.AplhabetKeys.Count;
                _encryptedCiphertext.Add(x);
                k++;
            }
            return _encryptedCiphertext;
        }

        private string PlaintextToCiphertext()
        {
            List<int> _enryptedPlaintext = PlaintextEncryption();
            if(_enryptedPlaintext == null) return null;

            string _ciphertext = "";
            foreach (int x in _enryptedPlaintext)
                _ciphertext += UiElements.IntegerKeys[x];
            return _ciphertext;
        }

        private string CiphertextToPlaintext()
        {
            List<int> _enryptedCiphertext = CiphertextToEncryption();
            if (_enryptedCiphertext == null) return null;
            string _plaintext = "";
            foreach (int x in _enryptedCiphertext)
                _plaintext += UiElements.IntegerKeys[x];
            return _plaintext;
        }

        private void Encript()
        {
            UiElements.EncriptedTextBox = PlaintextToCiphertext().ToUpper();
        }

        private void Decript()
        {
            UiElements.DecryptedTextBox = CiphertextToPlaintext();
        }


    }
}
