using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace test_playlist_1
{
    public partial class MusicPlayerApp : Form
    {
        public MusicPlayerApp()
        {
            InitializeComponent();
        }

        //Create Global Variables of String Type Array to save the titles or name of the Tracks and path of the track
        String[] paths, files;

        private void btnSelectSongs_Click(object sender, EventArgs e)
        {
            //Code to Select Songs
            OpenFileDialog newPlaylist = new OpenFileDialog();
            // the first place the program will select
            newPlaylist.InitialDirectory = "C://your_path_that_will_select_the_target_file";
            // it will determine the file type
            newPlaylist.Filter = "MP3 Video File (*.mp4)|*.mp4|Windows Media File (*.avi)|*.avi|MKV Video File (*.mkv)|*.mkv|All Files (*.*)|*.*";
            // restores the directory to the previously selected directory before closing
            newPlaylist.RestoreDirectory = false;
            // it allow muliple selection
            newPlaylist.Multiselect = true;
            // The dialog box return value is OK (usually sent from a button labeled OK)
            if (newPlaylist.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // it will store the name of files
                files = newPlaylist.SafeFileNames;
                // it will store the path of files
                paths = newPlaylist.FileNames;
                for (int list = 0; list < files.Length; list++)
                {
                    // it will add the paths of media files to the list
                    listBoxSongs.Items.Add(paths[list]);
                }
            }
        }

        // a function execute when the app on load
        private void MusicPlayerApp_Load(object sender, EventArgs e)
        {
            // it manage the directories and subdirectories
            DirectoryInfo dinfo =
                new DirectoryInfo(
                    "C://your_path_that_will_manage_directories_and_subdirectories");
            FileInfo[] Files = dinfo.GetFiles("*.xml");
            foreach (FileInfo file in Files)
            {
                // add the file name to the list of playlist
                playlist_list.Items.Add(file.Name);
            }
        }

        // the list box for video 
        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Write a code to play video
            try
            {
                axWindowsMediaMusic.URL = Convert.ToString(listBoxSongs.SelectedItem);
            }
            catch (Exception er)
            {
                MessageBox.Show("We can not play this Media");
            }
            
        }

        // The list box for play list
        private void playlist_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader playlist = new StreamReader(playlist_list.SelectedItem.ToString());
            while (playlist.Peek() >= 0)
                listBoxSongs.Items.Add(playlist.ReadLine());
        }

        // To close the program
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // To Save the play list xml file
        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamWriter Write;
            SaveFileDialog savePlaylist = new SaveFileDialog();
            // restores the directory to the previously selected directory before closing
            savePlaylist.RestoreDirectory = false;
            try
            {
                // the first place the program will select
                savePlaylist.InitialDirectory = "C://your_path_that_will_save_the_target_file";
                // it will determine the file type
                savePlaylist.Filter = ("XML File|*.xml|All Files|*.*");
                // show small window to choose file
                savePlaylist.ShowDialog();

                // To assign the xml file to the object Write
                Write = new StreamWriter(savePlaylist.FileName);
                
                // To write all the list box items to the object Write
                for (int I = 0; I < listBoxSongs.Items.Count; I++)
                {
                    // write the path of media file
                    Write.Write(paths[I]);
                    // write an empty line
                    Write.WriteLine();
                }
                Write.Close();
                MessageBox.Show("Playlist saved!");
            }

            catch 
            {
                return;
            }
        }

        // To load a play list from xml file
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadPlaylist = new OpenFileDialog();
            // it restricting muliple selection
            loadPlaylist.Multiselect = false;

            //// To eras all content of list box
            this.listBoxSongs.Items.Clear();
            
            try
            {
                // show small dialog to choose file
                loadPlaylist.ShowDialog();
                // the first place the program will select

                loadPlaylist.InitialDirectory = "C://your_path_that_will_load_the_target_file";
                
                 //To read in sequential the file from loadPlaylist to playlist to write on it
                StreamReader playlist = new StreamReader(loadPlaylist.FileName);

                while (playlist.Peek() >= 0)
                {
                    // write a single line to the playlist file
                    listBoxSongs.Items.Add(playlist.ReadLine());
                }

                listBoxSongs.Text = loadPlaylist.FileName;
            } catch {
                return;
            }
        }
    }
}
