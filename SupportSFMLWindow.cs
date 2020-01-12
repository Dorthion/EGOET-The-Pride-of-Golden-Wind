namespace EGOET
{
    public class SupportSFMLWindow : System.Windows.Forms.Control
    {
        private System.Windows.Forms.Cursor cursor = new System.Windows.Forms.Cursor("C:\\Users\\Dorthion\\Desktop\\Cursor3.cur");
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            this.Cursor = cursor;
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }
    }
}
