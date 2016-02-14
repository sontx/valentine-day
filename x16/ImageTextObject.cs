using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Valentine.x16
{
    public abstract class ImageTextObject : GameObject
    {
        protected readonly ContainerObject containerObject;

        public ImageTextObject(float x, float y, float scale, Image texture, string text, Color color, Font font) 
        {
            containerObject = new ContainerObject(x, y);
            if (texture != null)
                containerObject.Add(scale, texture);
            if (text != null)
                containerObject.Add(new SolidBrush(color), font, text);
        }
        
        public override void Draw(Graphics g, Rectangle bound)
        {
            containerObject.Draw(g);
        }

        public override void Update(Rectangle bound)
        {
            containerObject.Move(speedx, speedy);
        }

        public override void Destroy()
        {
            containerObject.Clear();
        }

        public override bool IsDisappear(RectangleF bound)
        {
            RectangleF rect = containerObject.DispRect;
            return rect.Bottom < bound.Y || rect.Top > bound.Bottom || rect.X > bound.Right || rect.Right < bound.X;
        }

        public override bool IsHorizontalCollision(RectangleF bound)
        {
            RectangleF rect = containerObject.DispRect;
            return rect.Bottom >= bound.Bottom || rect.Top <= bound.Top;
        }

        public override bool IsVerticalCollision(RectangleF bound)
        {
            RectangleF rect = containerObject.DispRect;
            return rect.X <= bound.X || rect.Right >= bound.Right;
        }

        protected abstract class VisionObject
        {
            protected RectangleF dispRect;
            public RectangleF DispRect { get { return dispRect; } }
            public virtual void Move(float dx, float dy)
            {
                dispRect.X += dx;
                dispRect.Y += dy;
            }
            public void PutAt(float x, float y)
            {
                dispRect.X = x;
                dispRect.Y = y;
            }
            public virtual void HorizontalMove(float dy)
            {
                dispRect.Y += dy;
            }
            public virtual void VerticalMove(float dx)
            {
                dispRect.X += dx;
            }
            public virtual void PutX(float x)
            {
                dispRect.X = x;
            }
            public virtual void PutY(float y)
            {
                dispRect.Y = y;
            }
            public abstract void Draw(Graphics g);
            public VisionObject(RectangleF rect)
            {
                this.dispRect = rect;
            }
        }

        protected class ImageObject : VisionObject
        {
            private readonly Image texture;
            private readonly RectangleF originRect;
            public ImageObject(float x, float y, float scale, Image texture) 
                : base(new RectangleF(x, y, texture.Width * scale, texture.Height * scale))
            {
                this.texture = texture;
                originRect = new RectangleF(0.0f, 0.0f, texture.Width, texture.Height);
            }
            public override void Draw(Graphics g)
            {
                g.DrawImage(texture, dispRect, originRect, GraphicsUnit.Pixel);
            }
        }

        protected class TextObject : VisionObject, IDisposable
        {
            private readonly Brush brush;
            private readonly Font font;
            private readonly string text;
            public TextObject(float x, float y, Brush brush, Font font, string text) 
                : base(new RectangleF(x, y, 0.0f, 0.0f))
            {
                this.brush = brush;
                this.font = font;
                this.text = text;
                SizeF sz = TextRenderer.MeasureText(text, font);
                dispRect.Width = sz.Width;
                dispRect.Height = sz.Height;
            }
            public Brush Brush { get { return brush; } }
            public void Dispose()
            {
                brush.Dispose();
            }
            public override void Draw(Graphics g)
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.DrawString(text, font, brush, dispRect.X, dispRect.Y);
            }
        }

        protected class ContainerObject : VisionObject
        {
            private static readonly float PADDING = 10.0f;
            private List<VisionObject> objects = new List<VisionObject>();
            public ContainerObject(float x, float y)
                : base(new RectangleF(x, y, 0.0f, 0.0f))
            {
            }
            private void StackObject(VisionObject obj)
            {
                if (objects.Count > 0)
                {
                    float deltaWidth = (obj.DispRect.Width - dispRect.Width) / 2.0f;
                    if (deltaWidth > 0.0f)
                        dispRect.Inflate(deltaWidth, 0.0f);
                    float newX = dispRect.X - (deltaWidth < 0.0f ? deltaWidth : 0.0f);
                    float newY = dispRect.Bottom + PADDING;
                    obj.PutAt(newX, newY);
                    dispRect.Height += obj.DispRect.Height + PADDING;
                }
                else
                {
                    dispRect = obj.DispRect;
                }
                objects.Add(obj);
            }
            public void Add(float scale, Image texture)
            {
                StackObject(new ImageObject(dispRect.X, dispRect.Y, scale, texture));
            }
            public void Add(Brush brush, Font font, string text)
            {
                StackObject(new TextObject(dispRect.X, dispRect.Y, brush, font, text));
            }
            public override void Draw(Graphics g)
            {
                foreach (VisionObject obj in objects)
                {
                    obj.Draw(g);
                }
            }
            public VisionObject GetChildAt(int index)
            {
                return objects[index];
            }
            public void Clear()
            {
                foreach (VisionObject obj in objects)
                {
                    TextObject textObject = obj as TextObject;
                    if (textObject != null)
                        textObject.Dispose();
                }
                objects.Clear();
            }
            public override void Move(float dx, float dy)
            {
                base.Move(dx, dy);
                foreach (VisionObject obj in objects)
                {
                    obj.Move(dx, dy);
                }
            }
            public override void HorizontalMove(float dy)
            {
                base.HorizontalMove(dy);
                foreach (VisionObject obj in objects)
                {
                    obj.HorizontalMove(dy);
                }
            }
            public override void PutX(float x)
            {
                base.PutX(x);
                foreach (VisionObject obj in objects)
                {
                    obj.PutX(x);
                }
            }
            public override void VerticalMove(float dx)
            {
                base.VerticalMove(dx);
                foreach (VisionObject obj in objects)
                {
                    obj.VerticalMove(dx);
                }
            }
            public override void PutY(float y)
            {
                base.PutY(y);
                foreach (VisionObject obj in objects)
                {
                    obj.PutY(y);
                }
            }
        }
    }
}
