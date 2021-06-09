using QuestionBankGenerator;
using System.Windows;
using System.Windows.Controls;

namespace QuestionBankPractice
{
    /// <summary>
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        QuestionBank[] questionBanks;
        Question currentQuestion;
        int questionIndex;
        QuestionType currentType;
        SkillLevel currentLevel;
        RadioButton[] options;

        public QuestionPage()
        {
            InitializeComponent();

            questionIndex = 0;
            currentLevel = SkillLevel.Junior;
            currentType = NaviPage.qType;
            options = new RadioButton[4] { OptionA, OptionB, OptionC, OptionD };

            questionBanks = new QuestionBank[(int)NaviPage.qLevel + 1];
            questionBanks[0] = QuestionBank.LoadQuestionBankXml
                        (@"C:\Users\yoush\Desktop\QuestionBankReader\juniorQuestionBank.xml");
            if (NaviPage.qLevel >= SkillLevel.Intermediate)
                questionBanks[1] = QuestionBank.LoadQuestionBankXml
                    (@"C:\Users\yoush\Desktop\QuestionBankReader\intermediateQuestionBank.xml");
            if (NaviPage.qLevel >= SkillLevel.Senior)
                questionBanks[2] = QuestionBank.LoadQuestionBankXml
                    (@"C:\Users\yoush\Desktop\QuestionBankReader\seniorQuestionBank.xml");
            if (NaviPage.qLevel >= SkillLevel.Tech)
                questionBanks[3] = QuestionBank.LoadQuestionBankXml
                    (@"C:\Users\yoush\Desktop\QuestionBankReader\techQuestionBank.xml");
            if (NaviPage.qLevel >= SkillLevel.SeniorTech)
                questionBanks[4] = QuestionBank.LoadQuestionBankXml
                    (@"C:\Users\yoush\Desktop\QuestionBankReader\seniorTechQuestionBank.xml");

            currentQuestion = questionBanks[0].QuestionArray[0][0];
            ShowQuestion(currentQuestion);
        }

        private void Commit_Click(object sender, RoutedEventArgs e)
        {
            bool isRight = false;
            int optionIndex = 0;

            string rightAnswer = currentQuestion.Answer.Substring(6);
            if (rightAnswer == "B") optionIndex = 1;
            else if (rightAnswer == "C") optionIndex = 2;
            else if (rightAnswer == "D") optionIndex = 3;

            isRight = (bool)options[optionIndex].IsChecked ? true : false;

            currentQuestion.Mastered = isRight;

            Commit.Visibility = Visibility.Collapsed;
            Answer.Visibility = Visibility.Visible;
            Annotation.Visibility = Visibility.Visible;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (questionIndex < questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType].Length - 1)
            {
                questionIndex++;
                currentQuestion = questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType][questionIndex];
                ShowQuestion(currentQuestion);
            }
            else if (currentLevel < NaviPage.qLevel)
            {
                currentLevel = (SkillLevel)((int)currentLevel + 1);

                questionIndex = 0;
                currentQuestion = questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType][questionIndex];
                ShowQuestion(currentQuestion);
            }
            else
                MessageBox.Show("当前为最后一题", "Tip", MessageBoxButton.OK);
        }

        private void LastQuestioin_Click(object sender, RoutedEventArgs e)
        {
            if (questionIndex > 0)
            {
                questionIndex--;
                currentQuestion = questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType][questionIndex];
                ShowQuestion(currentQuestion);
            }
            else if (currentLevel > SkillLevel.Junior)
            {
                currentLevel = (SkillLevel)((int)currentLevel - 1);
                questionIndex = questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType].Length - 1;
                currentQuestion = questionBanks[(int)currentLevel].QuestionArray[(int)NaviPage.qType][questionIndex];
            }
            else
            {
                MessageBox.Show("当前为第一题", "Tip", MessageBoxButton.OK);
            }
        }

        private void ShowQuestion(Question q)
        {
            //Stem.Text = q.Stem;

            for(int i = 0; i < options.Length; i++)
            {
                options[i].Content = q.Options[i];
                options[i].IsChecked = false;
            }
            
            Answer.Text = q.Answer;
            //Annotation.Text = juniorQuestionBank.QuestionArray[(int)QuestionType.SingleChoice][questionIndex].Annotation.ToString();
            Answer.Visibility = Visibility.Hidden;
            Annotation.Visibility = Visibility.Hidden;
            Commit.Visibility = Visibility.Visible;
        }

        private void BackToNavi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
