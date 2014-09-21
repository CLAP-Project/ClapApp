using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp
{
    public static class PanoramaBar
    {
        public static ApplicationBarIconButton MakeButton(string iconPng, string text, EventHandler e = null)
        {
            var button = new ApplicationBarIconButton()
            {
                IconUri = new Uri("/Images/" + iconPng, UriKind.Relative),
                Text = text
            };

            if (e != null)
                button.Click += e;

            return button;
        }

        public static ApplicationBarIconButton GetMinimizarButton(IApplicationBar self)
        {
            return MakeButton("minus.png", "minimizar", (object sender, EventArgs e) =>
            {
                self.Mode = self.Mode == ApplicationBarMode.Default ?
                    ApplicationBarMode.Minimized : ApplicationBarMode.Default;
            });
        }

        public static void AddButtons(this IApplicationBar self, ApplicationBarIconButton[] buttons, bool addMinimizar = false)
        {
            self.Buttons.Clear();
            // self.Mode = minimize? ApplicationBarMode.Minimized : ApplicationBarMode.Default ;

            if (buttons != null)
            {
                foreach (var button in buttons)
                    self.Buttons.Add(button);
            }

            if (addMinimizar)
                self.Buttons.Add(GetMinimizarButton(self));
        }
    }
}
