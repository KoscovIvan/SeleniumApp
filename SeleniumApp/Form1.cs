using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace SeleniumApp
{
    public partial class Form1 : Form
    {
        IWebDriver Browser;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Categories = CategoriesTextBox.Text;
            if (string.IsNullOrEmpty(Categories))
            {
                Categories = "Все отделы";
            }

            string Language = LanguageTextBox.Text;
            if (!string.IsNullOrEmpty(Language))
            {
                if (Language == "Английский")
                {
                    Language = "1";
                }

                if (Language == "Русский")
                {
                    Language = "2";
                }

                if (Language == "Немецкий")
                {
                    Language = "3";
                }
            }

            int ExpectedResult;

            bool result = int.TryParse(ExpectedResultTextBox.Text, out var number);

            if (result == true)
            {
                ExpectedResult = int.Parse(ExpectedResultTextBox.Text);
            }
            else
            {
                ExpectedResult = 0;
            }

            Browser = new OpenQA.Selenium.Firefox.FirefoxDriver();
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("https://careers.veeam.ru/vacancies");

            Thread.Sleep(2000);

            Actions actionProvider = new Actions(Browser);
            IWebElement SerchElement = Browser.FindElement(By.Id("sl"));
            SerchElement.Click();
            SerchElement = Browser.FindElement(By.LinkText(Categories));
            SerchElement.Click();
            SerchElement = Browser.FindElement(By.Id("sl"));
            IAction keydown = actionProvider.KeyDown(OpenQA.Selenium.Keys.Tab);
            Browser.FindElement(By.XPath(("(//button[@class='dropdown-toggle btn btn-primary'])[1]"))).Click();
            if (Language == "1" || Language == "2" || Language == "3")
            {
                Browser.FindElement(By.XPath(("(//label[@class='custom-control-label'])[" + Language + "]"))).Click();
            }

            int PahtCount = 2; // Счетчик искомых блоков пути
            int Counter = 0; // Счетчик успешных итераций

            for (int i = 0; i < 1;)
            {
                try
                {
                    SerchElement = Browser.FindElement(By.XPath(("(//a[contains(.,'Подробнее')])[" + PahtCount + "]")));
                    PahtCount = PahtCount + 2;
                    Counter++;
                }
                catch (Exception)
                {

                    i++;
                }
            }

            string OutMassege = "";

            if (Counter > ExpectedResult)
            {
                OutMassege = "Количество выданных вакансий " + Counter.ToString() + " больше ожидаемого " + ExpectedResult.ToString();
            }

            if (Counter < ExpectedResult)
            {
                OutMassege = "Количество выданных вакансий " + Counter.ToString() + " меньше ожидаемого " + ExpectedResult.ToString();
            }

            if (Counter == ExpectedResult)
            {
                OutMassege = "Количество выданных вакансий " + Counter.ToString() + " совпадает с ожидаемым " + ExpectedResult.ToString();
            }

            OutTextBox.Text = OutMassege;

            Browser.Quit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
