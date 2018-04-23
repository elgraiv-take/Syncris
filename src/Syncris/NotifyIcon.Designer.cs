namespace Syncris
{
    partial class NotifyIcon
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenu.SuspendLayout();
            // 
            // m_notifyIcon
            // 
            this.m_notifyIcon.ContextMenuStrip = this.m_contextMenu;
            this.m_notifyIcon.Text = "Syncris";
            this.m_notifyIcon.Visible = true;
            // 
            // m_contextMenu
            // 
            this.m_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuItemShow,
            this.m_menuItemExit});
            this.m_contextMenu.Name = "m_contextMenu";
            this.m_contextMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // m_menuItemShow
            // 
            this.m_menuItemShow.Name = "m_menuItemShow";
            this.m_menuItemShow.Size = new System.Drawing.Size(103, 22);
            this.m_menuItemShow.Text = "Show";
            // 
            // m_menuItemExit
            // 
            this.m_menuItemExit.Name = "m_menuItemExit";
            this.m_menuItemExit.Size = new System.Drawing.Size(103, 22);
            this.m_menuItemExit.Text = "Exit";
            this.m_contextMenu.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        private System.Windows.Forms.ContextMenuStrip m_contextMenu;
        private System.Windows.Forms.ToolStripMenuItem m_menuItemShow;
        private System.Windows.Forms.ToolStripMenuItem m_menuItemExit;
    }
}
