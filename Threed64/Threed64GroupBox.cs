using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threed64
{
    public class Threed64GroupBox : GroupBox
    {
        public enum BorderStyle { SINGLE, DOUBLE }

        public Color AABackColor { get; set; } = SystemColors.Control;
        public Color AALineColor { get; set; } = Color.Black;
        public Color AADoubleLineColor { get; set; } = Color.Black;
        public String AAText { get; set; } = "Text";
        public int AALeftBuffer { get; set; } = 15;
        public int AAStringPad { get; set; } = 5;
        public BorderStyle AABorderStyle { get; set; } = BorderStyle.SINGLE;
        public int AABorderStep { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //base.OnPaint(e);
            SizeF stringSize = e.Graphics.MeasureString(AAText, Font);
            int stringMid = (int)stringSize.Height / 2;
            Rectangle r = new Rectangle(0, 0, 0, 0);

            e.Graphics.DrawRectangle(new Pen(AALineColor),
                new Rectangle(
                    new Point(0, stringMid),
                    new Size(ClientRectangle.Width - 1, ClientRectangle.Height - stringMid - 1)));

            if (AABorderStyle == BorderStyle.DOUBLE)
            {
                e.Graphics.DrawRectangle(new Pen(AADoubleLineColor),
                    new Rectangle(
                    new Point(AABorderStep, stringMid + AABorderStep),
                    new Size(ClientRectangle.Width - 1 - 2*AABorderStep, ClientRectangle.Height - stringMid - 1 - 2*AABorderStep)
                    ));
            }

            e.Graphics.FillRectangle(new SolidBrush(AABackColor),
                new Rectangle(
                    new Point(AALeftBuffer - AAStringPad, 0),
                    new Size((int)stringSize.Width + AAStringPad + AAStringPad, (int)stringSize.Height)));

            e.Graphics.DrawString(AAText, Font, new SolidBrush(ForeColor), new Point(AALeftBuffer, 0));
        }
    }
}
