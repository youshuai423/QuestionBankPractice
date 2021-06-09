using QuestionBankGenerator;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QuestionBankPractice
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class NaviPage : Page
    {
        public static SkillLevel qLevel;
        public static QuestionType qType;
        //public static bool IsAllType = false;

        public NaviPage()
        {
            InitializeComponent();
        }

        private void StartPractice_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbJunior.IsChecked) qLevel = SkillLevel.Junior;
            else if ((bool)rbIntermediate.IsChecked) qLevel = SkillLevel.Intermediate;
            else if ((bool)rbSenior.IsChecked) qLevel = SkillLevel.Senior;
            else if ((bool)rbTech.IsChecked) qLevel = SkillLevel.Tech;
            else if ((bool)rbSeniorTech.IsChecked) qLevel = SkillLevel.SeniorTech;

            if ((bool)rbSingle.IsChecked) qType = QuestionType.SingleChoice;
            else if ((bool)rbMultiple.IsChecked) qType = QuestionType.MultipleChoice;
            else if ((bool)rbTrueFalse.IsChecked) qType = QuestionType.TrueFalse;
            else if ((bool)rbPicture.IsChecked) qType = QuestionType.Picture;
            else if ((bool)rbCalculation.IsChecked) qType = QuestionType.Calculation;

            this.NavigationService.Navigate(new Uri("QuestionPage.xaml", UriKind.Relative));            
        }
    }
}
