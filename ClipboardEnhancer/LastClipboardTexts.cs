using System;
using System.Collections.Generic;
using System.Text;

namespace ClipboardEnhancer
{
    /*
     * This class will represent a ring of text that collect the clipboard texts and
     * Return all the last unique clipboards.
     */

    public class LastClipboardTexts
    {
        private int m_nLastUniqueDisplayedClipboardText = 10;
        
        private string[] m_LastUniqueDisplayedClipboard;
        private int m_nLastClipboardTextIndex;
        public LastClipboardTexts()
        {
            m_nLastUniqueDisplayedClipboardText = 10;
            m_nLastClipboardTextIndex = -1;
            m_LastUniqueDisplayedClipboard = new string[m_nLastUniqueDisplayedClipboardText];
        }

        public void AddClipboardText(string cb)
        {
            //No text?
            if (cb == null || cb == "")
            {
                return;
            }

            //Copied text is the same than the last?
            if (m_nLastClipboardTextIndex > 0 && 
                m_nLastClipboardTextIndex < m_nLastUniqueDisplayedClipboardText)
            {
                if (m_LastUniqueDisplayedClipboard[m_nLastClipboardTextIndex] == cb)
                {
                    return;
                }
            }

            //The ring is full?
            if (m_nLastClipboardTextIndex + 1 == m_nLastUniqueDisplayedClipboardText)
            {
                m_nLastClipboardTextIndex = 0;
            }
            else
            {
                m_nLastClipboardTextIndex++;
            }

            m_LastUniqueDisplayedClipboard[m_nLastClipboardTextIndex] = cb;
        }

        public string GetClipboardTextHistory()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < m_nLastUniqueDisplayedClipboardText; i++)
            {
                int intexInRing = (m_nLastClipboardTextIndex + 1 + i) % m_nLastUniqueDisplayedClipboardText;
                if (m_LastUniqueDisplayedClipboard[intexInRing] != null &&
                    m_LastUniqueDisplayedClipboard[intexInRing] != "")
                {
                    stringBuilder.Append(m_LastUniqueDisplayedClipboard[intexInRing]
                        + System.Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }


    }
}
