using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31206
{
    public partial class Form1 : Form
    {
        private string fileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        //アプリケーション終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    return;
                }
            }

            Application.Exit();
        }

        //×ボタン終了
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    e.Cancel = true;
                    return;
                }
            }

            Application.Exit();
        }

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    SaveToolStripMenuItem_Click(sender, e);

                    rtTextArea.Text = "";
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    rtTextArea.Text = "";
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    return;
                }
            }

            this.fileName = "";
            this.Text = "テキストエディタ";
        }

        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;
                    this.Text = fileName;
                }
            }
        }

        //ファイル保存メソッド
        private void FileSave(string fileName)
        {
            //SteamReaderクラスを使用してファイルを読み込み
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
                this.fileName = fileName;
                this.Text = this.fileName;
            }
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイルあり
            if (File.Exists(fileName))
            {
                FileSave(fileName);
            }

            //ファイルなし
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }


        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //編集
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        //マスク機能
        private void EditMenuMaskCheck()
        {
            //元に戻すマスク
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;

            //やり直しマスク
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;

            //削除、切り取り、コピーマスク
            if (rtTextArea.SelectedText != "")
            {
                CutToolStripMenuItem.Enabled = true;
                CopyToolStripMenuItem.Enabled = true;
                DeleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                CutToolStripMenuItem.Enabled = false;
                CopyToolStripMenuItem.Enabled = false;
                DeleteToolStripMenuItem.Enabled = false;
            }

            //貼り付けマスク
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = fdFont.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.Font = fdFont.Font;
            }
        }

        //カラー
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = cdColor.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.ForeColor = cdColor.Color;
            }
        }

    }
}
