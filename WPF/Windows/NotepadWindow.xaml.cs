using Newtonsoft.Json;
using Notepad.BE;
using Notepad.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Notepad.Windows
{
    /// <summary>
    /// Interaction logic for NotepadWindow.xaml
    /// </summary>
    public partial class NotepadWindow : Window
    {
        private DatabaseCommunication databaseCommunication;
        private ApiUrlAdress apiUrlAdress;
        public ObservableCollection<Note> Notes { get; set; }
        private User userData;

        public NotepadWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            this.databaseCommunication = new DatabaseCommunication();
            this.apiUrlAdress = new ApiUrlAdress();
            this.Notes = new ObservableCollection<Note>();
            this.userData = user;

            this.LoadAllNotes();
        }

        private async void AddNoteClick(object sender, RoutedEventArgs e)
        {
            var noteToSend = new
            {
                username = this.userData.username,
                heading = "Enter heading",
                content = "Enter content"
            };

            var message = await this.databaseCommunication.PostDataAsync(noteToSend, this.apiUrlAdress.addNewNote);


            if(message == "Note added successfully")
            {
                this.LoadAllNotes();
            }
            else
            {
                MessageBox.Show(message);
            }

        }

        private async void DeleteNoteClick(object sender, RoutedEventArgs e)
        {
            var selectedNote = (Note)NotesListBox.SelectedItem;

            var noteToDelete = this.Notes.FirstOrDefault(note => note.Id == selectedNote.Id);



            if (noteToDelete != null)
            {
                this.Notes.Remove(noteToDelete);

                var deleteContent = new
                {
                    id = noteToDelete.Id.ToString(),
                    username = this.userData.username
                };

                using (var client = new HttpClient())
                {
                    var message = await this.databaseCommunication.PostDataAsync(deleteContent, this.apiUrlAdress.deleteNote);
                    MessageBox.Show(message);
                }

            }

            this.SortNotes();
        }

        private async void SaveNoteClick(object sender, RoutedEventArgs e)
        {
            var selectedNote = (Note)NotesListBox.SelectedItem;

            var updateContent = new
            {
                id = selectedNote.Id.ToString(),
                username = this.userData.username,
                content = selectedNote.Content
            };

            var updateHeading = new
            {
                id = selectedNote.Id.ToString(),
                username = this.userData.username,
                heading = selectedNote.Heading
            };

            var messageForUpdateContent = await this.databaseCommunication.PatchDataAsync(updateContent, this.apiUrlAdress.updateContent);
            var messageForUpdateHeading = await this.databaseCommunication.PatchDataAsync(updateHeading, this.apiUrlAdress.updateHeading);

            string message = messageForUpdateContent + "\n" + messageForUpdateHeading;

            MessageBox.Show(message);
        }

        private async void SaveAllNotesClick(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (var note in this.Notes)
                {
                    var updateContent = new
                    {
                        id = note.Id.ToString(),
                        username = this.userData.username,
                        content = note.Content
                    };

                    var updateHeading = new
                    {
                        id = note.Id.ToString(),
                        username = this.userData.username,
                        heading = note.Heading
                    };

                    var messageForUpdateContent = await this.databaseCommunication.PatchDataAsync(updateContent, this.apiUrlAdress.updateContent);
                    var messageForUpdateHeading = await this.databaseCommunication.PatchDataAsync(updateHeading, this.apiUrlAdress.updateHeading);

                }

                string message = "Notes saved succesfully";

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private async void StatementComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            if (comboBox.SelectedItem == null)
            {
                return;
            }

            var selectedItem = comboBox.SelectedItem as ComboBoxItem;
            var selectedNote = (Note)NotesListBox.SelectedItem;

            if (selectedNote != null)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Finished":
                        selectedNote.Color = Brushes.Red;
                        selectedNote.finished = "2";
                        this.UpdateColorInDatabase(selectedNote.Id.ToString(), this.apiUrlAdress.finishedStatement);
                        break;
                    case "Unfinished":
                        selectedNote.Color = Brushes.Green;
                        selectedNote.finished = "1";
                        this.UpdateColorInDatabase(selectedNote.Id.ToString(), this.apiUrlAdress.unfinishedStatement);
                        break;
                    case "Important":
                        selectedNote.Color = Brushes.Orange;
                        selectedNote.finished = "3";
                        this.UpdateColorInDatabase(selectedNote.Id.ToString(), this.apiUrlAdress.priorityStatement);
                        break;
                }

                this.SortNotes();
                comboBox.SelectedIndex = -1;
            }
        }

        private async void LoadAllNotes()
        {
            if (this.Notes != null)
            {
                this.Notes.Clear();
            }

            string url = this.apiUrlAdress.GetAllNotes(this.userData.username);
            var notes = await this.databaseCommunication.LoadDataFromDatabaseAsync<Note>(url);

            foreach (Note note in notes)
            { 
                note.Color = this.GetColorOfStatement(note.finished);
            }

            foreach (Note note in notes)
            {
                this.Notes.Add(note);
            }

            this.SortNotes();
        }

        private void SortNotes()
        {
            //var sortedNotes = this.Notes.OrderByDescending(note => int.Parse(note.finished));
            NotesListBox.ItemsSource = this.Notes.OrderByDescending(note => int.Parse(note.finished));


            //NotesListBox.ItemsSource = sortedNotes;
        }

        private Brush GetColorOfStatement(string statemnt)
        {
            switch(statemnt)
            {
                case "1":
                    return Brushes.Green;
                case "2":
                    return Brushes.Red;
                case "3":
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }

        private async void UpdateColorInDatabase(string id, string url)
        {
            var colorData = new
            {
                id = id,
                username = this.userData.username
            };

            await this.databaseCommunication.PatchDataAsync(colorData, url);
        }
    }
}
