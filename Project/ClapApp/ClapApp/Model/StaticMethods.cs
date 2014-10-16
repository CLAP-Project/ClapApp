using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClapApp.Model
{
    public static class StaticMethods
    {
        public static void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = sender as TextBox;
                var binding = tb.GetBindingExpression(TextBox.TextProperty);

                if (binding != null)
                {
                    binding.UpdateSource();
                }
            }
        }
    }
}
