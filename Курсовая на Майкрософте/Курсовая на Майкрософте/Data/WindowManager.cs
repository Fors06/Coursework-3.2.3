using System.ComponentModel;
using System.Windows;

namespace Курсовая_на_Майкрософте.Data
{
    public static class WindowManager
    {
        private static Window currentWindow;

        /// <summary>
        /// Переключает текущее окно на указанное и закрывает предыдущее.
        /// </summary>
        public static void SwitchWindow(Window window)
        {
            if (currentWindow != null)
            {
                currentWindow.Closing -= PreventClosingOnSwitch;
                currentWindow.Close();
            }

            currentWindow = window;
            currentWindow.Closing += PreventClosingOnSwitch;
            currentWindow.Show();
        }

        private static void PreventClosingOnSwitch(object sender, CancelEventArgs e)
        {
            // Предотвращаем случайное закрытие окна при попытке закрыть само себя
            if (!Equals(sender, currentWindow)) e.Cancel = true;
        }
    }
}