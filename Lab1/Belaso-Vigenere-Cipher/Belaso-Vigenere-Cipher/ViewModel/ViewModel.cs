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
        /// <summary>
        /// Transforms the string received from the Key Texbox into a list of integers assoiated
        /// to each letter of the string
        /// </summary>
        /// <returns>
        /// A list of intigers representing the encryption of the key
        /// </returns>

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
        /// <summary>
        /// The method iterates through the letters of the plaintext.
        /// At the same time it iterates through the numerical form of the key.
        ///When the number of letters iterated in the plaintext divides to the number of lettes iterated in the key,
        ///it means that we went through all the letters in the key and we need to start again.
        ///While iterating thorugh the plaintext letters and the key,
        ///we add for each pair of letters (from plaintext and key ) their associated number in the alphabet
        ///and insert in a list of integers the modulo the numbers of letters in the aphabet for each sum.
        /// </summary>
        /// <returns>
        /// The returned list will be the numerical form of the encrypted plaintext
        /// returns>
        public List<int> PlaintextEncryption()
        {
            List<int> _numericalKey = TransformKeyIntoNumerical();
            if (_numericalKey == null) 
                return null;
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
        
        /// <summary>
        /// The method iterates through the letters of the cyphertext.
        /// At the same time it iterates through the numerical form of the key.
        ///When the number of letters iterated in the cyphertext divides to the number of lettes iterated in the key,
        ///it means that we went through all the letters in the key and we need to start again.
        ///While iterating thorugh the cyphertext letters and the key,
        ///we substract for each pair of letters (from cyphertext and key ) their associated number in the alphabet
        ///and insert in a list of integers the modulo the numbers of letters in the aphabet for each sum.
        /// </summary>
        /// <returns>
        /// The returned list will be the numerical form of the encrypted cyphertext
        /// returns>
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

        /// <summary>
        /// The method takes the numerical encryption of the plaintext and creates the 
        /// associated string that will be the ciphertext.
        /// </summary>
        /// <returns>
        /// A string representing the ciphertext
        /// returns>
        private string PlaintextToCiphertext()
        {
            List<int> _enryptedPlaintext = PlaintextEncryption();
            if(_enryptedPlaintext == null) return null;

            string _ciphertext = "";
            foreach (int x in _enryptedPlaintext)
                _ciphertext += UiElements.IntegerKeys[x];
            return _ciphertext;
        }

        /// <summary>
        /// The method takes the numerical encryption of the ciphertext and creates the 
        /// associated string that will be the plaintext.
        /// </summary>
        /// <returns>
        /// A string representing the plaintext
        /// returns>
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
