using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Victorina
{

    public partial class MainWindow : Window
    {
        List<string> questions = new List<string>();
        List<string> answers = new List<string>();
        List<string> images = new List<string>();
        int score = 0;
        int questionNumber = 0;

        string correctAnswer = "";
        int grade = 0;

        public MainWindow()
        {
            InitializeComponent();

            questions.Add("Как называется этот тип машины?");
            answers.Add("Седан");
            images.Add("Sedan.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Хэтчбек");
            images.Add("Хэтчбек.jpeg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Купе");
            images.Add("Купе.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Кабриолет");
            images.Add("Кабриолет.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Пикап");
            images.Add("Пикап.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Внедорожник");
            images.Add("Внедорожник.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Минивэн");
            images.Add("Минивэн.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Лимузин");
            images.Add("Лимузин.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Грузовик");
            images.Add("Грузовик.jpg");

            questions.Add("Как называется этот тип машины?");
            answers.Add("Автобус");
            images.Add("Автобус.jpg");

            Randomize();
        }

        private void Randomize()
        {
            Random random = new Random();

            List<Question> questionsList = new List<Question>();

            for (int i = 0; i < questions.Count; i++)
            {
                questionsList.Add(new Question(questions[i], answers[i], images[i]));
            }

            for (int i = 0; i < questionsList.Count; i++)
            {
                int index = random.Next(0, questionsList.Count);

                Question tempQuestion = questionsList[i];
                questionsList[i] = questionsList[index];
                questionsList[index] = tempQuestion;
            }

            for (int i = 0; i < questionsList.Count; i++)
            {
                questions[i] = questionsList[i].QuestionText;
                answers[i] = questionsList[i].AnswerText;
                images[i] = questionsList[i].ImageName;
            }

            NextQuestion();
        }

        private void NextQuestion()
        {
            if (questionNumber < questions.Count)
            {
                labelQuestion.Content = questions[questionNumber];

                imageQuestion.Source = new BitmapImage(new Uri("Cars/" + images[questionNumber], UriKind.Relative));

                ClearButtons();

                GenerateChoices();
            }
            else
            {
                grade = GetGrade(score);

                MessageBox.Show("Конец игры! Ваш счет: " + score + " из " + questions.Count + ". Ваша оценка: " + grade, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);

                Restart();
            }
        }

        private void GenerateChoices()
        {
            Random random = new Random();

            List<string> allAnswers = new List<string>();

            correctAnswer = answers[questionNumber];
            allAnswers.Add(correctAnswer);
            allAnswers.AddRange(GetWrongAnswers(3));
            allAnswers = Shuffle(allAnswers);
            button1.Content = allAnswers[0];
            button2.Content = allAnswers[1];
            button3.Content = allAnswers[2];
            button4.Content = allAnswers[3];
        }

        private List<string> GetWrongAnswers(int count)
        {
            List<string> wrongAnswers = answers.ToList(); 

            wrongAnswers.Remove(correctAnswer); 

            Random random = new Random();
            List<string> selectedAnswers = new List<string>();

            for (int i = 0; i < count; i++)
            {
                int index = random.Next(0, wrongAnswers.Count);
                selectedAnswers.Add(wrongAnswers[index]);
                wrongAnswers.RemoveAt(index); 
            }

            return selectedAnswers;
        }

        private List<string> Shuffle(List<string> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        private void ClearButtons()
        {
            button1.Content = "";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "";

            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;
        }

        private void CheckAnswer(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == correctAnswer)
            {
                score++;

                MessageBox.Show("Правильно!", "Ответ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Неправильно! Правильный ответ: " + correctAnswer, "Ответ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            questionNumber++;

            NextQuestion();

            labelScore.Content = "Счет: " + score + " из " + questions.Count;
        }

        private void Restart()
        {
            labelScore.Content = "Счет: 0 из " + questions.Count;
            score = 0;
            questionNumber = 0;
            grade = 0;

            Randomize();
        }

        // оценка
        private int GetGrade(int score)
        {
            if (score < 4)
            {
                return 2;
            }
            else if (score >= 4 && score < 6)
            {
                return 3;
            }
            else if (score >= 6 && score < 8)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
    }

    // класс для вопросов, ответов и изображений
    public class Question
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string ImageName { get; set; }

        public Question(string questionText, string answerText, string imageName)
        {
            QuestionText = questionText;
            AnswerText = answerText;
            ImageName = imageName;
        }
    }
}




