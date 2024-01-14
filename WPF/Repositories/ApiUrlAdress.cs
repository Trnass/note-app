namespace Notepad.Repositories
{
    public class ApiUrlAdress
    {
        public string registerUser = "https://semestralka-prg.trnass.cz/users/register/";
        public string loginUser = "https://semestralka-prg.trnass.cz/users/login/";
        public string addNewNote = "https://semestralka-prg.trnass.cz/notes/insert/";
        public string unfinishedStatement = "https://semestralka-prg.trnass.cz/notes/update/unfinished/";
        public string finishedStatement = "https://semestralka-prg.trnass.cz/notes/update/finished/";
        public string priorityStatement = "https://semestralka-prg.trnass.cz/notes/update/priority/";
        public string updateHeading = "https://semestralka-prg.trnass.cz/notes/update/heading/";
        public string updateContent = "https://semestralka-prg.trnass.cz/notes/update/content/";
        public string deleteNote = "https://semestralka-prg.trnass.cz/notes/delete/";

        public string GetAllNotes(string username)
        {
            return $"https://semestralka-prg.trnass.cz/notes/all/?username={username}";
        }
    }
}
