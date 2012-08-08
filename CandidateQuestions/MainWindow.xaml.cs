using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace com.hatterassoftware.candidatequestions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> candidateList = new List<String>();
        private List<Question> questionList = new List<Question>();

        private TextBlock[] textBlocks;

        private int currCandidate = -1;
        private int currQuestion = -1;
        private int startCandidate = -1;
        
        private DispatcherTimer countdownTimer;
        private int savedTime;
        private int state = 1; // 1 = start of round screen; 2 = start of question; 3 = asking question; 4 = question done; 5 = end of questions
        private int minutes;
        private int seconds;
        private int millis;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                String[] questions = System.IO.File.ReadAllLines(@"questions.txt");
                String[] candidates = System.IO.File.ReadAllLines(@"candidates.txt");

                foreach (String line in questions)
                {
                    if (line.StartsWith("#")) continue;

                    String timeStr;
                    String fontSizeStr;
                    String question;
                    String tmpLine;

                    timeStr = line.Substring(0, line.IndexOf(" "));
                    tmpLine = line.Substring(line.IndexOf(" ") + 1);
                    fontSizeStr = tmpLine.Substring(0, line.IndexOf(" "));
                    question = tmpLine.Substring(line.IndexOf(" ") + 1);

                    Debug.WriteLine("timeStr=" + timeStr + ", fontSizeStr=" + fontSizeStr + ", question=" + question);

                    Question q = new Question(int.Parse(timeStr), int.Parse(fontSizeStr), question);
                    questionList.Add(q);
                }

                foreach (String line in candidates)
                {
                    if (line.StartsWith("#")) continue;
                    candidateList.Add(line);
                }

                textBlocks = new TextBlock[candidateList.Count];

                int i = 0;
                foreach (String candidate in candidateList)
                {
                    textBlocks[i] = new TextBlock();
                    textBlocks[i].Text = candidate;
                    textBlocks[i].Margin = new Thickness(10);
                    textBlocks[i].FontSize = 20;

                    candidateStack.Children.Add(textBlocks[i]);
                    i++;
                }

                this.setCurrentCandidate(currCandidate);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }

        public void setCurrentCandidate(int offset)
        {
            for (int i = 0; i < textBlocks.Length; i++)
            {
                if (i == offset)
                {
                    textBlocks[i].Background = Brushes.LightGreen;
                    textBlocks[i].Foreground = Brushes.Black;
                }
                else
                {
                    textBlocks[i].Background = Brushes.White;
                    textBlocks[i].Foreground = Brushes.LightGray;
                }
            }
        }

        private void startRoundClicked(Object sender, EventArgs e)
        {
            currQuestion++;
            if(currQuestion == questionList.Count) return;
            startCandidate++;
            if(startCandidate == candidateList.Count) startCandidate = 0;
            currCandidate = startCandidate;

            drawQuestion();

            startRound.Visibility = System.Windows.Visibility.Collapsed;
            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;
            reset.Visibility = System.Windows.Visibility.Collapsed;
            next.Visibility = System.Windows.Visibility.Collapsed;
            if(currQuestion > 0) {
                prev.Visibility = System.Windows.Visibility.Visible;
            } else {
                prev.Visibility = System.Windows.Visibility.Collapsed;
            }
            quit.Visibility = System.Windows.Visibility.Collapsed;

            this.setCurrentCandidate(currCandidate);

            initializeTime(questionList.ElementAt(currQuestion).getTime());

            setTime();

            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromMilliseconds(100);
            countdownTimer.Tick += new EventHandler(countdownTimerStep);

            state = 2;
        }

        private void drawQuestion()
        {
            String[] splitter = new String[] { "\\n" };
            String[] qStr = questionList.ElementAt(currQuestion).getQuestion().Split(splitter, StringSplitOptions.None);

            question.FontSize = questionList.ElementAt(currQuestion).getFontSize();
            question.Text = "";

            for (int i = 0; i < qStr.Length; i++)
            {
                question.Inlines.Add(new Run(qStr[i]));
                if (i < qStr.Length - 1)
                {
                    question.Inlines.Add(new LineBreak());
                }
            }
        }

        private void initializeTime(int time)
        {
            this.savedTime = time;
            this.minutes = time / 60;
            this.seconds = time % 60;
            this.millis = 0;
        }

        private void countdownTimerStep(Object sender, EventArgs e)
        {
            millis -= 100;
            if (!(millis == 0 && seconds == 0 && minutes == 0))
            {
                if (millis < 0)
                {
                    millis = 900;
                    seconds--;
                    if (seconds < 0)
                    {
                        seconds = 59;
                        minutes--;
                        if (minutes < 0)
                        {
                            minutes = 0;
                        }
                    }
                }
            }
            else
            {
                countdownTimer.Stop();

                pause.Visibility = System.Windows.Visibility.Collapsed;
                reset.Visibility = System.Windows.Visibility.Collapsed;
                play.Visibility = System.Windows.Visibility.Collapsed;
                next.Visibility = System.Windows.Visibility.Visible;
                prev.Visibility = System.Windows.Visibility.Visible;
                state = 4;
            }

            setTime();
        }

        private void setTime()
        {
            String timeString = "";
            if (minutes < 10) timeString += "0";
            timeString += minutes + ":";
            if (seconds < 10) timeString += "0";
            timeString += seconds + ":";
            if (millis < 100) timeString += "00";
            timeString += millis;

            if (minutes == 0 && seconds <= 10)
                timer.Foreground = Brushes.Red;
            else
                timer.Foreground = Brushes.Black;

            timer.Text = timeString;
        }

        private void startClicked(Object sender, EventArgs e)
        {
            play.Visibility = System.Windows.Visibility.Collapsed;
            pause.Visibility = System.Windows.Visibility.Visible;
            reset.Visibility = System.Windows.Visibility.Collapsed;
            next.Visibility = System.Windows.Visibility.Collapsed;
            prev.Visibility = System.Windows.Visibility.Collapsed;

            countdownTimer.Start();
        }

        private void pauseClicked(Object sender, EventArgs e)
        {
            countdownTimer.Stop();

            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;
            reset.Visibility = System.Windows.Visibility.Visible;
            next.Visibility = System.Windows.Visibility.Visible;
            if (currQuestion == 0 && currCandidate == 0)
            {
                prev.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                prev.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void resetClicked(Object sender, EventArgs e)
        {
            initializeTime(this.savedTime);
            setTime();
        }

        private void nextClicked(Object sender, EventArgs e)
        {
            state = 2;
            currCandidate++;
            if (currCandidate == candidateList.Count)
                currCandidate = 0;
            if (currCandidate == startCandidate)
            {
                if (currQuestion + 1 == questionList.Count)
                {
                    question.Text = "End of Questions";
                    startRound.Visibility = System.Windows.Visibility.Collapsed;
                    quit.Visibility = System.Windows.Visibility.Visible;
                    prev.Visibility = System.Windows.Visibility.Visible;
                    state = 5;
                }
                else
                {
                    question.Text = "Next Round";
                    startRound.Visibility = System.Windows.Visibility.Visible;
                    prev.Visibility = System.Windows.Visibility.Visible;
                    quit.Visibility = System.Windows.Visibility.Collapsed;
                    state = 1;
                }
                play.Visibility = System.Windows.Visibility.Collapsed;
                pause.Visibility = System.Windows.Visibility.Collapsed;
                reset.Visibility = System.Windows.Visibility.Collapsed;
                next.Visibility = System.Windows.Visibility.Collapsed;
                
                setCurrentCandidate(-1);
                minutes = 0;
                seconds = 0;
                millis = 0;

                setTime();

                return;
            }

            startRound.Visibility = System.Windows.Visibility.Collapsed;
            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;
            reset.Visibility = System.Windows.Visibility.Collapsed;
            next.Visibility = System.Windows.Visibility.Collapsed;
            quit.Visibility = System.Windows.Visibility.Collapsed;
            
            setCurrentCandidate(currCandidate);

            initializeTime(this.savedTime);
            setTime();
        }

        private void prevClicked(Object sender, EventArgs e)
        {
            //  Start of round or finished with question or at end of questions
            if (state == 1 || state == 5)
            {
                currCandidate--;
            }
            if (state == 2 || state == 3)
            {
                // Is this the first person in the round?
                if (currCandidate == startCandidate)
                {
                    currQuestion--;
                    startCandidate--;
                    currCandidate--;
                    if (startCandidate < 0)
                    {
                        startCandidate = candidateList.Count - 1;
                    }
                }
                currCandidate--;
            }
            if (currCandidate < 0)
            {
                currCandidate = currCandidate + candidateList.Count;
            }
            drawQuestion();
            setCurrentCandidate(currCandidate);
            initializeTime(this.savedTime);
            setTime();
            startRound.Visibility = System.Windows.Visibility.Collapsed;
            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;
            reset.Visibility = System.Windows.Visibility.Collapsed;
            next.Visibility = System.Windows.Visibility.Collapsed;
            if (currQuestion == 0  && currCandidate == 0)
            {
                prev.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                prev.Visibility = System.Windows.Visibility.Visible;
            }
            quit.Visibility = System.Windows.Visibility.Collapsed;

            state = 2;
        }

        private void quitClicked(Object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
