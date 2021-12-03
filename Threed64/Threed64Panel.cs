using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threed64
{
    public class Threed64Panel : Panel
    {
        public Color[] AAHighColorSet { get; set; } = new Color[] {
            Color.FromArgb(240, 240, 240),
            Color.FromArgb(220, 220, 220),
            Color.FromArgb(190, 190, 190),
            Color.FromArgb(120, 120, 120)
        };

        public Color[] AALowColorSet { get; set; } = new Color[] {
            Color.FromArgb(255, 255, 255),
            Color.FromArgb(250, 250, 250),
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(240, 240, 240)
        };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (AAHighColorSet.Length != AALowColorSet.Length)
                return;

            for (int i = 0; i < AAHighColorSet.Length; i++)
            {
                Pen highColor = new Pen(AAHighColorSet[i]);
                Pen lowColor = new Pen(AALowColorSet[i]);

                //top l-r line
                e.Graphics.DrawLine(highColor,
                    new Point(i, i),
                    new Point(ClientSize.Width-1-i, i)
                );

                //top-down line l
                e.Graphics.DrawLine(highColor,
                    new Point(i, i),
                    new Point(i, ClientSize.Height - 1 - i)
                );

                //bottom l-r line
                e.Graphics.DrawLine(lowColor,
                    new Point(i, ClientSize.Height - 1 - i),
                    new Point(ClientSize.Width - 1 -i, ClientSize.Height - 1 -i)  
                );

                //top-down line r
                e.Graphics.DrawLine(lowColor,
                    new Point(ClientSize.Width - 1 -i, i),
                    new Point(ClientSize.Width - 1 -i, ClientSize.Height - 1 -i)
                );
            }
        }
    }
}
