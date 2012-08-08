using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.hatterassoftware.candidatequestions
{
    class Question
    {
        private int time;
        private int fontSize = 36;
        private String question;

        public Question(int time, int fontSize, String question)
        {
            this.setTime(time);
            this.setFontSize(fontSize);
            this.setQuestion(question);
        }

        public int getTime()
        {
            return this.time;
        }

        public void setTime(int time)
        {
            this.time = time;
        }

        public String getQuestion()
        {
            return this.question;
        }

        public void setQuestion(String question)
        {
            this.question = question;
        }

        public int getFontSize()
        {
            return this.fontSize;
        }

        public void setFontSize(int fontSize)
        {
            this.fontSize = fontSize;
        }
    }
}
