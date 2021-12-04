using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Threed64
{
    public class Threed64Button : UserControl
    {
        private enum HoverMode { HOVER_UP, HOVER_DOWN };
        public enum ImageMode { IMAGE, TEXT, IMAGE_AND_TEXT };

        public bool AAClickable { get; set; } = true;
        public Color AAHighlightShadowColor { get; set; } = SystemColors.ControlLightLight;
        public Color AABottomShadowColor { get; set; } = SystemColors.ControlDarkDark;
        public Color AABaseButtonColor { get; set; } = SystemColors.Control;
        public Color AABaseButtonDownColor { get; set; } = SystemColors.ScrollBar;
        public Color AALineBorderColor { get; set; } = Color.Black;
        public Image AAIconImage { get; set; } = new Bitmap(1,1);
        public ImageMode AAImageMode { get; set; } = ImageMode.TEXT;
        public int AAImageTextPxBuffer { get; set; } = 15;
        public int AABorderWidth { get; set; } = 5;
        public int AALineBorderWidth { get; set; } = 2;
        public string AATextString { get; set; } = "Text";
        public bool AATextShadow { get; set; } = false;
        public Color AATextShadowColor { get; set; } = Color.Gray;
        public int AATextShadowOffset { get; set; } = 1;

        private HoverMode _curHover = HoverMode.HOVER_UP;
        private int _drawOffset = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;           
            base.OnPaint(e);

            SolidBrush baseColor = _curHover == HoverMode.HOVER_UP ?
                new SolidBrush(AABaseButtonColor) : new SolidBrush(AABaseButtonDownColor);
            SolidBrush highColor = _curHover == HoverMode.HOVER_UP ?
                new SolidBrush(AAHighlightShadowColor) : new SolidBrush(AABottomShadowColor);
            SolidBrush lowColor = _curHover == HoverMode.HOVER_UP ?
                new SolidBrush(AABottomShadowColor) : new SolidBrush(AAHighlightShadowColor);

            e.Graphics.FillRectangle(baseColor, ClientRectangle);

            //top edge
            e.Graphics.FillRectangle(highColor,
                new Rectangle(new Point(0, 0),
                new Size(ClientRectangle.Width, AABorderWidth)));
            e.Graphics.FillRectangle(highColor,
                new Rectangle(new Point(0, 0),
                new Size(AABorderWidth, ClientRectangle.Height)));

            //bottom edge
            e.Graphics.FillRectangle(lowColor,
                new Rectangle(new Point(AABorderWidth, ClientRectangle.Height - AABorderWidth),
                new Size(ClientRectangle.Width, AABorderWidth)));
            e.Graphics.FillRectangle(lowColor,
                new Rectangle(new Point(ClientRectangle.Width - AABorderWidth, AABorderWidth),
                new Size(AABorderWidth, ClientRectangle.Height)));

            //corners
            e.Graphics.FillPolygon(lowColor, new PointF[] {
                new PointF(0, ClientRectangle.Height),
                new PointF(AABorderWidth, ClientRectangle.Height-AABorderWidth),
                new PointF(AABorderWidth, ClientRectangle.Height)
            });

            e.Graphics.FillPolygon(lowColor, new PointF[] {
                new PointF(ClientRectangle.Width - AABorderWidth, AABorderWidth),
                new PointF(ClientRectangle.Width, AABorderWidth),
                new PointF(ClientRectangle.Width, 0)
            });

            switch (AAImageMode)
            {
                case ImageMode.TEXT:
                    {
                        SizeF strSize = e.Graphics.MeasureString(AATextString, Font);

                        if (AATextShadow)
                        { 
                            e.Graphics.DrawString(AATextString, Font,
                                new SolidBrush(AATextShadowColor),
                            new PointF((ClientRectangle.Width / 2) - (strSize.Width / 2) + _drawOffset + AATextShadowOffset,
                                (ClientRectangle.Height / 2) - (strSize.Height / 2) + _drawOffset + AATextShadowOffset));
                        }

                        e.Graphics.DrawString
                            (AATextString, Font,
                            new SolidBrush(ForeColor),
                            new PointF((ClientRectangle.Width / 2) - (strSize.Width / 2) + _drawOffset,
                                (ClientRectangle.Height / 2) - (strSize.Height / 2) + _drawOffset));

                    }
                    break;
                case ImageMode.IMAGE:
                    {
                        e.Graphics.DrawImageUnscaled(AAIconImage, new Point(
                         (ClientRectangle.Width / 2) - (AAIconImage.Width / 2) + _drawOffset,
                         (ClientRectangle.Height / 2) - (AAIconImage.Height / 2) + _drawOffset
                         ));
                    }
                    break;
                case ImageMode.IMAGE_AND_TEXT:
                    {
                        SizeF strSize = e.Graphics.MeasureString(AATextString, Font);
                        int xOrig = (ClientRectangle.Width / 2) - (AAIconImage.Width / 2) - ((int)strSize.Width / 2) - AAImageTextPxBuffer;
                        e.Graphics.DrawImage(AAIconImage,
                            new Point(xOrig + _drawOffset, ClientRectangle.Height / 2 - (AAIconImage.Height / 2) + _drawOffset));
                        e.Graphics.DrawString(AATextString, Font, new SolidBrush(ForeColor),
                            new Point(xOrig + AAImageTextPxBuffer + AAIconImage.Width + _drawOffset,
                            (ClientRectangle.Height / 2) - ((int)strSize.Height / 2) + _drawOffset));
                    }
                    break;
            }

            //if(AALineBorderWidth > 0)
            //    e.Graphics.DrawRectangle(new Pen(AALineBorderColor, AALineBorderWidth),
            //        new Rectangle(new Point(1, 1), 
            //        new Size(ClientRectangle.Width - AALineBorderWidth, 
            //            ClientRectangle.Height - AALineBorderWidth)));

            for (int i = 0; i < AALineBorderWidth; i++)
            {
                e.Graphics.DrawRectangle(new Pen(AALineBorderColor),
                    new Rectangle(new Point(i, i), new Size(ClientRectangle.Width - i - i - 1, ClientRectangle.Height - i - i - 1)));
            }

            if (!Enabled)
            {
                Brush b = new SolidBrush(Color.FromArgb(25, Color.Black));
                e.Graphics.FillRectangle(b, ClientRectangle);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!AAClickable)
                return;
            base.OnMouseDown(e);
            _curHover = HoverMode.HOVER_DOWN;
            _drawOffset = AABorderWidth / 2;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!AAClickable)
                return;
            base.OnMouseUp(e);
            _curHover = HoverMode.HOVER_UP;
            _drawOffset = 0;
            Invalidate();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Threed64Button
            // 
            this.Name = "Threed64Button";
            this.Size = new System.Drawing.Size(130, 28);
            this.ResumeLayout(false);

        }
    }
}
