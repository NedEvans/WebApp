using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using WebApp.Pages;

namespace WebApp
{
    public class OpenAIService
    {
        public string key { get; set; } = string.Empty;
        public Noun nextNoun=new Noun();
        public string jsonString { get; set; } = string.Empty ;
        public string responseContent { get; set; }
        public Uri nextImage {  get; set; }

        public List<Noun> nouns = new List<Noun>();


        public OpenAIService()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            key = config["chatGPT35key"];
            nouns = CreateNounList("Text_Files\\Nouns.txt");
            var random = new Random();
            int nounIndex = random.Next(1, 1999); // Generates a number between 1
            nextNoun = nouns.First(item => item.id == nounIndex);
            getNextImage("an image for the noun " + nextNoun + ".");
        }

        public async Task getNextImage(string _request)
        {
            string endpoint = "https://api.openai.com/v1/images/generations";
            var data = new
            {
                model = "dall-e-3",
                prompt = _request,
                n = 1,
                size = "1024x1024"

            };
            jsonString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + key);
            var AIresponse = await client.PostAsync(endpoint, content);
            responseContent = await AIresponse.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
            nextImage = jsonResponse.data[0].url;
        }



        public List<Noun> CreateNounList(string _path)
        {
            List<Noun> _list = new();
            int _id = 0;
            string _idstring = "";
            string _line;
            StreamReader _sr = new StreamReader(_path);
            while ((_line = _sr.ReadLine()) != null)   //read the full line
            {
                var item = _line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                _idstring = item[0];
                _id = int.Parse(_idstring.Trim(new Char[] { '.' }));
                _list.Add(new Noun
                {
                    id = _id,
                    english = item[1],
                    french = item[3],
                    gender = item[5],
                });
            }
            _sr.Close();
            return _list;
        }
        public List<Tense> CreateTenseList()
        {
            List<Tense> _list = new List<Tense>();
            _list.Add(new Tense { id = 1, Mood = "Present"});
            _list.Add(new Tense { id = 2, Mood = "Imparfait" });
            _list.Add(new Tense { id = 3, Mood = "Passe Compose" });
            _list.Add(new Tense { id = 4, Mood = "Future Simple" });
            _list.Add(new Tense { id = 5, Mood = "Plus Que Parfait" });
            return _list;
        }

        public List<Subject> CreateSubjectList()
        {
            List<Subject> _list = new List<Subject>();
            _list.Add(new Subject { id = 1, person = "First Person Singular", alias = "Je" });
            _list.Add(new Subject { id = 2, person = "Second Person Singular (informal)", alias = "Tu" });
            _list.Add(new Subject { id = 3, person = "Third Person Singular", alias = "Il" });
            _list.Add(new Subject { id = 4, person = "Third Person Singular", alias = "Elle" });
            _list.Add(new Subject { id = 5, person = "Third Person Singular", alias = "On" });
            _list.Add(new Subject { id = 6, person = "First Person Plural", alias = "Nous" });
            _list.Add(new Subject { id = 7, person = "Second Person Plural", alias = "Vous" });
            _list.Add(new Subject { id = 8, person = "Third Person Plural", alias = "Ils" });
            _list.Add(new Subject { id = 9, person = "Third Person Plural", alias = "Elles" });
            _list.Add(new Subject { id = 10, person = "Third Person Singular", alias = "Mon Pére" });
            return _list;
        }

        public List<Verb> CreateVerbList()
        {
            List<Verb> _list = new();
            _list.Add(new Verb { id = 1, Infinitive = "abandonner", EnglishTranslation = "to give up / to abandon" });
            _list.Add(new Verb { id = 2, Infinitive = "abattre", EnglishTranslation = "to shoot" });
            _list.Add(new Verb { id = 3, Infinitive = "abîmer", EnglishTranslation = "to damage" });
            _list.Add(new Verb { id = 4, Infinitive = "accélérer", EnglishTranslation = "to accelerate" });
            _list.Add(new Verb { id = 5, Infinitive = "accepter", EnglishTranslation = "to accept" });
            _list.Add(new Verb { id = 6, Infinitive = "accompagner", EnglishTranslation = "to accompany" });
            _list.Add(new Verb { id = 7, Infinitive = "accoucher", EnglishTranslation = "to give birth" });
            _list.Add(new Verb { id = 8, Infinitive = "accrocher", EnglishTranslation = "to attach / to hang" });
            _list.Add(new Verb { id = 9, Infinitive = "acheter", EnglishTranslation = "to buy" });
            _list.Add(new Verb { id = 10, Infinitive = "agir", EnglishTranslation = "to act" });
            _list.Add(new Verb { id = 11, Infinitive = "aider", EnglishTranslation = "to help" });
            _list.Add(new Verb { id = 12, Infinitive = "aimer", EnglishTranslation = "to like / to love" });
            _list.Add(new Verb { id = 13, Infinitive = "ajouter", EnglishTranslation = "to add" });
            _list.Add(new Verb { id = 14, Infinitive = "aller", EnglishTranslation = "to go" });
            _list.Add(new Verb { id = 15, Infinitive = "allumer", EnglishTranslation = "to turn on / to switch on / to light up" });
            _list.Add(new Verb { id = 16, Infinitive = "apercevoir", EnglishTranslation = "to see / to spot" });
            _list.Add(new Verb { id = 17, Infinitive = "apprendre", EnglishTranslation = "to learn" });
            _list.Add(new Verb { id = 18, Infinitive = "arrêter", EnglishTranslation = "to stop / to arrest" });
            _list.Add(new Verb { id = 19, Infinitive = "arriver", EnglishTranslation = "to arrive" });
            _list.Add(new Verb { id = 20, Infinitive = "attendre", EnglishTranslation = "to wait" });
            _list.Add(new Verb { id = 21, Infinitive = "atterrir", EnglishTranslation = "to land" });
            _list.Add(new Verb { id = 22, Infinitive = "attraper", EnglishTranslation = "to catch" });
            _list.Add(new Verb { id = 23, Infinitive = "avaler", EnglishTranslation = "to swallow" });
            _list.Add(new Verb { id = 24, Infinitive = "avoir", EnglishTranslation = "to have" });
            _list.Add(new Verb { id = 25, Infinitive = "bégayer", EnglishTranslation = "to stutter" });
            _list.Add(new Verb { id = 26, Infinitive = "blesser", EnglishTranslation = "to injure" });
            _list.Add(new Verb { id = 27, Infinitive = "bloquer", EnglishTranslation = "to block" });
            _list.Add(new Verb { id = 28, Infinitive = "boire", EnglishTranslation = "to drink" });
            _list.Add(new Verb { id = 29, Infinitive = "bondir", EnglishTranslation = "to bounce" });
            _list.Add(new Verb { id = 30, Infinitive = "boucher", EnglishTranslation = "to clog" });
            _list.Add(new Verb { id = 31, Infinitive = "bouder", EnglishTranslation = "to sulk" });
            _list.Add(new Verb { id = 32, Infinitive = "bouillir", EnglishTranslation = "to boil" });
            _list.Add(new Verb { id = 33, Infinitive = "bouquiner", EnglishTranslation = "to read" });
            _list.Add(new Verb { id = 34, Infinitive = "briser", EnglishTranslation = "to break" });
            _list.Add(new Verb { id = 35, Infinitive = "bronzer", EnglishTranslation = "to sunbathe" });
            _list.Add(new Verb { id = 36, Infinitive = "cacher", EnglishTranslation = "to hide" });
            _list.Add(new Verb { id = 37, Infinitive = "cambrioler", EnglishTranslation = "to burglarize / to break in" });
            _list.Add(new Verb { id = 38, Infinitive = "casser", EnglishTranslation = "to break" });
            _list.Add(new Verb { id = 39, Infinitive = "causer", EnglishTranslation = "to cause" });
            _list.Add(new Verb { id = 40, Infinitive = "changer", EnglishTranslation = "to change" });
            _list.Add(new Verb { id = 41, Infinitive = "chanter", EnglishTranslation = "to sing" });
            _list.Add(new Verb { id = 42, Infinitive = "chauffer", EnglishTranslation = "to heat" });
            _list.Add(new Verb { id = 43, Infinitive = "chavirer", EnglishTranslation = "to capsize" });
            _list.Add(new Verb { id = 44, Infinitive = "chercher", EnglishTranslation = "to look for" });
            _list.Add(new Verb { id = 45, Infinitive = "chialer", EnglishTranslation = "to weep / to cry" });
            _list.Add(new Verb { id = 46, Infinitive = "chuchoter", EnglishTranslation = "to whisper" });
            _list.Add(new Verb { id = 47, Infinitive = "cicatriser", EnglishTranslation = "to heal" });
            _list.Add(new Verb { id = 48, Infinitive = "coiffer", EnglishTranslation = "to style / to do hair" });
            _list.Add(new Verb { id = 49, Infinitive = "coincer", EnglishTranslation = "to stick" });
            _list.Add(new Verb { id = 50, Infinitive = "coller", EnglishTranslation = "to glue" });
            _list.Add(new Verb { id = 51, Infinitive = "combattre", EnglishTranslation = "to fight" });
            _list.Add(new Verb { id = 52, Infinitive = "commander", EnglishTranslation = "to order" });
            _list.Add(new Verb { id = 53, Infinitive = "comparer", EnglishTranslation = "to compare" });
            _list.Add(new Verb { id = 54, Infinitive = "comprendre", EnglishTranslation = "to understand" });
            _list.Add(new Verb { id = 55, Infinitive = "compter", EnglishTranslation = "to count" });
            _list.Add(new Verb { id = 56, Infinitive = "concrétiser", EnglishTranslation = "to realise" });
            _list.Add(new Verb { id = 57, Infinitive = "conduire", EnglishTranslation = "to drive" });
            _list.Add(new Verb { id = 58, Infinitive = "confier", EnglishTranslation = "to entrust / to leave" });
            _list.Add(new Verb { id = 59, Infinitive = "confimer", EnglishTranslation = "to confirm" });
            _list.Add(new Verb { id = 60, Infinitive = "confisquer", EnglishTranslation = "to confiscate" });
            _list.Add(new Verb { id = 61, Infinitive = "conjuguer", EnglishTranslation = "to conjugate" });
            _list.Add(new Verb { id = 62, Infinitive = "connaitre", EnglishTranslation = "to know" });
            _list.Add(new Verb { id = 63, Infinitive = "continuer", EnglishTranslation = "to continue" });
            _list.Add(new Verb { id = 64, Infinitive = "contrôler", EnglishTranslation = "to control" });
            _list.Add(new Verb { id = 65, Infinitive = "couper", EnglishTranslation = "to cut" });
            _list.Add(new Verb { id = 66, Infinitive = "courir", EnglishTranslation = "to run" });
            _list.Add(new Verb { id = 67, Infinitive = "créer", EnglishTranslation = "to create" });
            _list.Add(new Verb { id = 68, Infinitive = "crier", EnglishTranslation = "to shout" });
            _list.Add(new Verb { id = 69, Infinitive = "croire", EnglishTranslation = "to believe" });
            _list.Add(new Verb { id = 70, Infinitive = "cuisiner", EnglishTranslation = "to cook" });
            _list.Add(new Verb { id = 71, Infinitive = "danser", EnglishTranslation = "to dance" });
            _list.Add(new Verb { id = 72, Infinitive = "décevoir", EnglishTranslation = "to disappoint" });
            _list.Add(new Verb { id = 73, Infinitive = "défaire", EnglishTranslation = "to unpack" });
            _list.Add(new Verb { id = 74, Infinitive = "demander", EnglishTranslation = "to ask" });
            _list.Add(new Verb { id = 75, Infinitive = "déménager", EnglishTranslation = "to move" });
            _list.Add(new Verb { id = 76, Infinitive = "démolir", EnglishTranslation = "to demolish" });
            _list.Add(new Verb { id = 77, Infinitive = "démotiver", EnglishTranslation = "to demotivate / to discourage" });
            _list.Add(new Verb { id = 78, Infinitive = "déprimer", EnglishTranslation = "to depress" });
            _list.Add(new Verb { id = 79, Infinitive = "descendre", EnglishTranslation = "to go down / come down" });
            _list.Add(new Verb { id = 80, Infinitive = "désinstaller", EnglishTranslation = "to uninstall" });
            _list.Add(new Verb { id = 81, Infinitive = "désobéir", EnglishTranslation = "to disobey" });
            _list.Add(new Verb { id = 82, Infinitive = "dessiner", EnglishTranslation = "to draw" });
            _list.Add(new Verb { id = 83, Infinitive = "détester", EnglishTranslation = "to hate" });
            _list.Add(new Verb { id = 84, Infinitive = "détruire", EnglishTranslation = "to destroy" });
            _list.Add(new Verb { id = 85, Infinitive = "devenir", EnglishTranslation = "to become" });
            _list.Add(new Verb { id = 86, Infinitive = "dire", EnglishTranslation = "to say/ to tell" });
            _list.Add(new Verb { id = 87, Infinitive = "disparaître", EnglishTranslation = "to disappear" });
            _list.Add(new Verb { id = 88, Infinitive = "disqualifier", EnglishTranslation = "to disqualify" });
            _list.Add(new Verb { id = 89, Infinitive = "donner", EnglishTranslation = "to give" });
            _list.Add(new Verb { id = 90, Infinitive = "dormir", EnglishTranslation = "to sleep" });
            _list.Add(new Verb { id = 91, Infinitive = "draguer", EnglishTranslation = "to hit on / to flirt" });
            _list.Add(new Verb { id = 92, Infinitive = "écarter", EnglishTranslation = "to spread" });
            _list.Add(new Verb { id = 93, Infinitive = "échanger", EnglishTranslation = "to exchange" });
            _list.Add(new Verb { id = 94, Infinitive = "économiser", EnglishTranslation = "to save up" });
            _list.Add(new Verb { id = 95, Infinitive = "écouter", EnglishTranslation = "to listen to" });
            _list.Add(new Verb { id = 96, Infinitive = "écrire", EnglishTranslation = "to write" });
            _list.Add(new Verb { id = 97, Infinitive = "emballer", EnglishTranslation = "to pack" });
            _list.Add(new Verb { id = 98, Infinitive = "embrasser", EnglishTranslation = "to kiss" });
            _list.Add(new Verb { id = 99, Infinitive = "encourager", EnglishTranslation = "to encourage" });
            _list.Add(new Verb { id = 100, Infinitive = "ennuyer", EnglishTranslation = "to annoy" });
            _list.Add(new Verb { id = 101, Infinitive = "entendre", EnglishTranslation = "to hear" });
            _list.Add(new Verb { id = 102, Infinitive = "envoyer", EnglishTranslation = "to send" });
            _list.Add(new Verb { id = 103, Infinitive = "éplucher", EnglishTranslation = "to peel" });
            _list.Add(new Verb { id = 104, Infinitive = "essayer", EnglishTranslation = "to try" });
            _list.Add(new Verb { id = 105, Infinitive = "étaler", EnglishTranslation = "to spread" });
            _list.Add(new Verb { id = 106, Infinitive = "éteindre", EnglishTranslation = "to turn off / to switch off" });
            _list.Add(new Verb { id = 107, Infinitive = "étendre", EnglishTranslation = "to hang" });
            _list.Add(new Verb { id = 108, Infinitive = "être", EnglishTranslation = "to be" });
            _list.Add(new Verb { id = 109, Infinitive = "étudier", EnglishTranslation = "to study" });
            _list.Add(new Verb { id = 110, Infinitive = "exister", EnglishTranslation = "to exist" });
            _list.Add(new Verb { id = 111, Infinitive = "expliquer", EnglishTranslation = "to explain" });
            _list.Add(new Verb { id = 112, Infinitive = "fabriquer", EnglishTranslation = "to make / to build" });
            _list.Add(new Verb { id = 113, Infinitive = "faire", EnglishTranslation = "to do" });
            _list.Add(new Verb { id = 114, Infinitive = "falsifier", EnglishTranslation = "to falsify" });
            _list.Add(new Verb { id = 115, Infinitive = "féliciter", EnglishTranslation = "to congratulate" });
            _list.Add(new Verb { id = 116, Infinitive = "fermer", EnglishTranslation = "to close" });
            _list.Add(new Verb { id = 117, Infinitive = "fêter", EnglishTranslation = "to celebrate" });
            _list.Add(new Verb { id = 118, Infinitive = "finir", EnglishTranslation = "to finish" });
            _list.Add(new Verb { id = 119, Infinitive = "fondre", EnglishTranslation = "to melt" });
            _list.Add(new Verb { id = 120, Infinitive = "frapper", EnglishTranslation = "to hit" });
            _list.Add(new Verb { id = 121, Infinitive = "fumer", EnglishTranslation = "to smoke" });
            _list.Add(new Verb { id = 122, Infinitive = "gagner", EnglishTranslation = "to win" });
            _list.Add(new Verb { id = 123, Infinitive = "garder", EnglishTranslation = "to keep" });
            _list.Add(new Verb { id = 124, Infinitive = "gâter", EnglishTranslation = "to spoil" });
            _list.Add(new Verb { id = 125, Infinitive = "geler", EnglishTranslation = "to freeze" });
            _list.Add(new Verb { id = 126, Infinitive = "glisser", EnglishTranslation = "to slip" });
            _list.Add(new Verb { id = 127, Infinitive = "goûter", EnglishTranslation = "to taste" });
            _list.Add(new Verb { id = 128, Infinitive = "grimper", EnglishTranslation = "to climb" });
            _list.Add(new Verb { id = 129, Infinitive = "habiller", EnglishTranslation = "to dress" });
            _list.Add(new Verb { id = 130, Infinitive = "harceler", EnglishTranslation = "to harass" });
            _list.Add(new Verb { id = 131, Infinitive = "hospitaliser", EnglishTranslation = "to hospitalize" });
            _list.Add(new Verb { id = 132, Infinitive = "hurler", EnglishTranslation = "to scream" });
            _list.Add(new Verb { id = 133, Infinitive = "hypnotiser", EnglishTranslation = "to hypnotize" });
            _list.Add(new Verb { id = 134, Infinitive = "idolâtrer", EnglishTranslation = "to idolize" });
            _list.Add(new Verb { id = 135, Infinitive = "illuminer", EnglishTranslation = "to illuminate" });
            _list.Add(new Verb { id = 136, Infinitive = "inaugurer", EnglishTranslation = "to inaugurate" });
            _list.Add(new Verb { id = 137, Infinitive = "infecter", EnglishTranslation = "to infect" });
            _list.Add(new Verb { id = 138, Infinitive = "installer", EnglishTranslation = "to install" });
            _list.Add(new Verb { id = 139, Infinitive = "intercepter", EnglishTranslation = "to intercept" });
            _list.Add(new Verb { id = 140, Infinitive = "introduire", EnglishTranslation = "to insert" });
            _list.Add(new Verb { id = 141, Infinitive = "investir", EnglishTranslation = "to invest" });
            _list.Add(new Verb { id = 142, Infinitive = "jaillir", EnglishTranslation = "to erupt / to gush" });
            _list.Add(new Verb { id = 143, Infinitive = "jeter", EnglishTranslation = "to throw away" });
            _list.Add(new Verb { id = 144, Infinitive = "jeûner", EnglishTranslation = "to fast" });
            _list.Add(new Verb { id = 145, Infinitive = "jouer", EnglishTranslation = "to play" });
            _list.Add(new Verb { id = 146, Infinitive = "juger", EnglishTranslation = "to judge" });
            _list.Add(new Verb { id = 147, Infinitive = "kidnapper", EnglishTranslation = "to kidnap" });
            _list.Add(new Verb { id = 148, Infinitive = "klaxonner", EnglishTranslation = "to honk" });
            _list.Add(new Verb { id = 149, Infinitive = "laisser", EnglishTranslation = "to let" });
            _list.Add(new Verb { id = 150, Infinitive = "lancer", EnglishTranslation = "to throw" });
            _list.Add(new Verb { id = 151, Infinitive = "libérer", EnglishTranslation = "to release" });
            _list.Add(new Verb { id = 152, Infinitive = "licencier", EnglishTranslation = "to fire / to be a member" });
            _list.Add(new Verb { id = 153, Infinitive = "lier", EnglishTranslation = "to tie" });
            _list.Add(new Verb { id = 154, Infinitive = "ligoter", EnglishTranslation = "to tie up" });
            _list.Add(new Verb { id = 155, Infinitive = "lire", EnglishTranslation = "to read" });
            _list.Add(new Verb { id = 156, Infinitive = "livrer", EnglishTranslation = "to deliver" });
            _list.Add(new Verb { id = 157, Infinitive = "louer", EnglishTranslation = "to rent" });
            _list.Add(new Verb { id = 158, Infinitive = "louper", EnglishTranslation = "to miss" });
            _list.Add(new Verb { id = 159, Infinitive = "maigrir", EnglishTranslation = "to lose weight" });
            _list.Add(new Verb { id = 160, Infinitive = "maîtriser", EnglishTranslation = "to control / to master" });
            _list.Add(new Verb { id = 161, Infinitive = "manger", EnglishTranslation = "to eat" });
            _list.Add(new Verb { id = 162, Infinitive = "manifester", EnglishTranslation = "to protest" });
            _list.Add(new Verb { id = 163, Infinitive = "manquer", EnglishTranslation = "to miss" });
            _list.Add(new Verb { id = 164, Infinitive = "marcher", EnglishTranslation = "to walk" });
            _list.Add(new Verb { id = 165, Infinitive = "méditer", EnglishTranslation = "to meditate" });
            _list.Add(new Verb { id = 166, Infinitive = "mélanger", EnglishTranslation = "to mix" });
            _list.Add(new Verb { id = 167, Infinitive = "mentir", EnglishTranslation = "to lie" });
            _list.Add(new Verb { id = 168, Infinitive = "mettre", EnglishTranslation = "to put/ to place" });
            _list.Add(new Verb { id = 169, Infinitive = "nager", EnglishTranslation = "to swim" });
            _list.Add(new Verb { id = 170, Infinitive = "narguer", EnglishTranslation = "to taunt" });
            _list.Add(new Verb { id = 171, Infinitive = "naviguer", EnglishTranslation = "to sail" });
            _list.Add(new Verb { id = 172, Infinitive = "négocier", EnglishTranslation = "to negotiate" });
            _list.Add(new Verb { id = 173, Infinitive = "neiger", EnglishTranslation = "to snow" });
            _list.Add(new Verb { id = 174, Infinitive = "nettoyer", EnglishTranslation = "to clean" });
            _list.Add(new Verb { id = 175, Infinitive = "nier", EnglishTranslation = "to deny" });
            _list.Add(new Verb { id = 176, Infinitive = "obéir", EnglishTranslation = "to obey" });
            _list.Add(new Verb { id = 177, Infinitive = "observer", EnglishTranslation = "to observe" });
            _list.Add(new Verb { id = 178, Infinitive = "obtenir", EnglishTranslation = "to get" });
            _list.Add(new Verb { id = 179, Infinitive = "occuper", EnglishTranslation = "to occupy" });
            _list.Add(new Verb { id = 180, Infinitive = "offrir", EnglishTranslation = "to offer" });
            _list.Add(new Verb { id = 181, Infinitive = "oublier", EnglishTranslation = "to forget" });
            _list.Add(new Verb { id = 182, Infinitive = "ouvrir", EnglishTranslation = "to open" });
            _list.Add(new Verb { id = 183, Infinitive = "pardonner", EnglishTranslation = "to forgive" });
            _list.Add(new Verb { id = 184, Infinitive = "parier", EnglishTranslation = "to bet" });
            _list.Add(new Verb { id = 185, Infinitive = "parler", EnglishTranslation = "to speak" });
            _list.Add(new Verb { id = 186, Infinitive = "partager", EnglishTranslation = "to share" });
            _list.Add(new Verb { id = 187, Infinitive = "participer", EnglishTranslation = "to participate" });
            _list.Add(new Verb { id = 188, Infinitive = "partir", EnglishTranslation = "to leave" });
            _list.Add(new Verb { id = 189, Infinitive = "passer", EnglishTranslation = "to pass" });
            _list.Add(new Verb { id = 190, Infinitive = "payer", EnglishTranslation = "to pay" });
            _list.Add(new Verb { id = 191, Infinitive = "pêcher", EnglishTranslation = "to fish" });
            _list.Add(new Verb { id = 192, Infinitive = "peindre", EnglishTranslation = "to paint" });
            _list.Add(new Verb { id = 193, Infinitive = "pénétrer", EnglishTranslation = "to penetrate" });
            _list.Add(new Verb { id = 194, Infinitive = "penser", EnglishTranslation = "to think" });
            _list.Add(new Verb { id = 195, Infinitive = "perdre", EnglishTranslation = "to lose" });
            _list.Add(new Verb { id = 196, Infinitive = "permettre", EnglishTranslation = "to allow" });
            _list.Add(new Verb { id = 197, Infinitive = "peser", EnglishTranslation = "to weigh" });
            _list.Add(new Verb { id = 198, Infinitive = "photographier", EnglishTranslation = "to photograph" });
            _list.Add(new Verb { id = 199, Infinitive = "plaisanter", EnglishTranslation = "to joke" });
            _list.Add(new Verb { id = 200, Infinitive = "planer", EnglishTranslation = "to hover" });
            _list.Add(new Verb { id = 201, Infinitive = "planifier", EnglishTranslation = "to plan" });
            _list.Add(new Verb { id = 202, Infinitive = "planter", EnglishTranslation = "to plant" });
            _list.Add(new Verb { id = 203, Infinitive = "pleurer", EnglishTranslation = "to cry" });
            _list.Add(new Verb { id = 204, Infinitive = "pleuvoir", EnglishTranslation = "to rain" });
            _list.Add(new Verb { id = 205, Infinitive = "plier", EnglishTranslation = "to fold" });
            _list.Add(new Verb { id = 206, Infinitive = "polluer", EnglishTranslation = "to pollute" });
            _list.Add(new Verb { id = 207, Infinitive = "pondre", EnglishTranslation = "to lay" });
            _list.Add(new Verb { id = 208, Infinitive = "porter", EnglishTranslation = "to carry" });
            _list.Add(new Verb { id = 209, Infinitive = "poser", EnglishTranslation = "to ask / to put" });
            _list.Add(new Verb { id = 210, Infinitive = "poster", EnglishTranslation = "to post" });
            _list.Add(new Verb { id = 211, Infinitive = "pousser", EnglishTranslation = "to push" });
            _list.Add(new Verb { id = 212, Infinitive = "pouvoir", EnglishTranslation = "to be able to" });
            _list.Add(new Verb { id = 213, Infinitive = "prendre", EnglishTranslation = "to take" });
            _list.Add(new Verb { id = 214, Infinitive = "préparer", EnglishTranslation = "to prepare" });
            _list.Add(new Verb { id = 215, Infinitive = "présenter", EnglishTranslation = "to present / to introduce" });
            _list.Add(new Verb { id = 216, Infinitive = "prêter", EnglishTranslation = "to lend" });
            _list.Add(new Verb { id = 217, Infinitive = "prier", EnglishTranslation = "to pray" });
            _list.Add(new Verb { id = 218, Infinitive = "promener", EnglishTranslation = "to walk" });
            _list.Add(new Verb { id = 219, Infinitive = "prononcer", EnglishTranslation = "to pronounce" });
            _list.Add(new Verb { id = 220, Infinitive = "prouver", EnglishTranslation = "to prove" });
            _list.Add(new Verb { id = 221, Infinitive = "qualifier", EnglishTranslation = "to qualify" });
            _list.Add(new Verb { id = 222, Infinitive = "questionner", EnglishTranslation = "to question" });
            _list.Add(new Verb { id = 223, Infinitive = "quitter", EnglishTranslation = "to leave" });
            _list.Add(new Verb { id = 224, Infinitive = "raccourcir", EnglishTranslation = "to shorten" });
            _list.Add(new Verb { id = 225, Infinitive = "racheter", EnglishTranslation = "to purchase" });
            _list.Add(new Verb { id = 226, Infinitive = "rajouter", EnglishTranslation = "to add" });
            _list.Add(new Verb { id = 227, Infinitive = "ralentir", EnglishTranslation = "to slow down" });
            _list.Add(new Verb { id = 228, Infinitive = "râler", EnglishTranslation = "to grumble" });
            _list.Add(new Verb { id = 229, Infinitive = "ramasser", EnglishTranslation = "to pick up" });
            _list.Add(new Verb { id = 230, Infinitive = "ranger", EnglishTranslation = "to tidy up" });
            _list.Add(new Verb { id = 231, Infinitive = "râper", EnglishTranslation = "to grate" });
            _list.Add(new Verb { id = 232, Infinitive = "rappeler", EnglishTranslation = "to call back" });
            _list.Add(new Verb { id = 233, Infinitive = "recevoir", EnglishTranslation = "to receive" });
            _list.Add(new Verb { id = 234, Infinitive = "reconnaitre", EnglishTranslation = "to recognize" });
            _list.Add(new Verb { id = 235, Infinitive = "redire", EnglishTranslation = "to repeat / to say again" });
            _list.Add(new Verb { id = 236, Infinitive = "refaire", EnglishTranslation = "to redo" });
            _list.Add(new Verb { id = 237, Infinitive = "regarder", EnglishTranslation = "to look" });
            _list.Add(new Verb { id = 238, Infinitive = "remettre", EnglishTranslation = "to put back" });
            _list.Add(new Verb { id = 239, Infinitive = "remuer", EnglishTranslation = "to stir" });
            _list.Add(new Verb { id = 240, Infinitive = "rencontrer", EnglishTranslation = "to meet" });
            _list.Add(new Verb { id = 241, Infinitive = "rendre", EnglishTranslation = "to give back" });
            _list.Add(new Verb { id = 242, Infinitive = "réparer", EnglishTranslation = "to repair" });
            _list.Add(new Verb { id = 243, Infinitive = "repasser", EnglishTranslation = "to iron" });
            _list.Add(new Verb { id = 244, Infinitive = "répéter", EnglishTranslation = "to repeat" });
            _list.Add(new Verb { id = 245, Infinitive = "répondre", EnglishTranslation = "to answer" });
            _list.Add(new Verb { id = 246, Infinitive = "rester", EnglishTranslation = "to stay" });
            _list.Add(new Verb { id = 247, Infinitive = "retourner", EnglishTranslation = "to return" });
            _list.Add(new Verb { id = 248, Infinitive = "retrouver", EnglishTranslation = "to find" });
            _list.Add(new Verb { id = 249, Infinitive = "revenir", EnglishTranslation = "to come back" });
            _list.Add(new Verb { id = 250, Infinitive = "rêver", EnglishTranslation = "to dream" });
            _list.Add(new Verb { id = 251, Infinitive = "rire", EnglishTranslation = "to laugh" });
            _list.Add(new Verb { id = 252, Infinitive = "rouler", EnglishTranslation = "to drive / to ride" });
            _list.Add(new Verb { id = 253, Infinitive = "rouvrir", EnglishTranslation = "to reopen" });
            _list.Add(new Verb { id = 254, Infinitive = "saigner", EnglishTranslation = "to bleed" });
            _list.Add(new Verb { id = 255, Infinitive = "saisir", EnglishTranslation = "to cease" });
            _list.Add(new Verb { id = 256, Infinitive = "salir", EnglishTranslation = "so soil / to make dirty" });
            _list.Add(new Verb { id = 257, Infinitive = "saluer", EnglishTranslation = "to greet" });
            _list.Add(new Verb { id = 258, Infinitive = "sauter", EnglishTranslation = "to jump" });
            _list.Add(new Verb { id = 259, Infinitive = "sauver", EnglishTranslation = "to rescue" });
            _list.Add(new Verb { id = 260, Infinitive = "savoir", EnglishTranslation = "to know" });
            _list.Add(new Verb { id = 261, Infinitive = "secourir", EnglishTranslation = "to rescue" });
            _list.Add(new Verb { id = 262, Infinitive = "sécuriser", EnglishTranslation = "to secure" });
            _list.Add(new Verb { id = 263, Infinitive = "séduire", EnglishTranslation = "to seduce" });
            _list.Add(new Verb { id = 264, Infinitive = "séjourner", EnglishTranslation = "to stay" });
            _list.Add(new Verb { id = 265, Infinitive = "sélectionner", EnglishTranslation = "to select" });
            _list.Add(new Verb { id = 266, Infinitive = "sembler", EnglishTranslation = "to seem" });
            _list.Add(new Verb { id = 267, Infinitive = "sentir", EnglishTranslation = "to feel / to smell" });
            _list.Add(new Verb { id = 268, Infinitive = "servir", EnglishTranslation = "to serve" });
            _list.Add(new Verb { id = 269, Infinitive = "siffler", EnglishTranslation = "to whistle" });
            _list.Add(new Verb { id = 270, Infinitive = "signaler", EnglishTranslation = "to report" });
            _list.Add(new Verb { id = 271, Infinitive = "souffrir", EnglishTranslation = "to suffer" });
            _list.Add(new Verb { id = 272, Infinitive = "souhaiter", EnglishTranslation = "to wish" });
            _list.Add(new Verb { id = 273, Infinitive = "sourire", EnglishTranslation = "to smile" });
            _list.Add(new Verb { id = 274, Infinitive = "soutenir", EnglishTranslation = "to support" });
            _list.Add(new Verb { id = 275, Infinitive = "suivre", EnglishTranslation = "to follow" });
            _list.Add(new Verb { id = 276, Infinitive = "supprimer", EnglishTranslation = "to delete" });
            _list.Add(new Verb { id = 277, Infinitive = "surpeupler", EnglishTranslation = "to overcrowd" });
            _list.Add(new Verb { id = 278, Infinitive = "survivre", EnglishTranslation = "to survive" });
            _list.Add(new Verb { id = 279, Infinitive = "taper", EnglishTranslation = "to hit" });
            _list.Add(new Verb { id = 280, Infinitive = "teindre", EnglishTranslation = "to dye" });
            _list.Add(new Verb { id = 281, Infinitive = "téléphoner", EnglishTranslation = "to phone" });
            _list.Add(new Verb { id = 282, Infinitive = "tenir", EnglishTranslation = "to hold" });
            _list.Add(new Verb { id = 283, Infinitive = "tenter", EnglishTranslation = "to try" });
            _list.Add(new Verb { id = 284, Infinitive = "tester", EnglishTranslation = "to test" });
            _list.Add(new Verb { id = 285, Infinitive = "tirer", EnglishTranslation = "to pull, to shoot" });
            _list.Add(new Verb { id = 286, Infinitive = "tomber", EnglishTranslation = "to fall" });
            _list.Add(new Verb { id = 287, Infinitive = "tondre", EnglishTranslation = "to mow" });
            _list.Add(new Verb { id = 288, Infinitive = "torturer", EnglishTranslation = "to torture" });
            _list.Add(new Verb { id = 289, Infinitive = "tourner", EnglishTranslation = "to turn" });
            _list.Add(new Verb { id = 290, Infinitive = "tousser", EnglishTranslation = "to cough" });
            _list.Add(new Verb { id = 291, Infinitive = "traduire", EnglishTranslation = "to translate" });
            _list.Add(new Verb { id = 292, Infinitive = "travailler", EnglishTranslation = "to work" });
            _list.Add(new Verb { id = 293, Infinitive = "trouver", EnglishTranslation = "to find" });
            _list.Add(new Verb { id = 294, Infinitive = "tuer", EnglishTranslation = "to kill" });
            _list.Add(new Verb { id = 295, Infinitive = "uriner", EnglishTranslation = "to urinate" });
            _list.Add(new Verb { id = 296, Infinitive = "usurper", EnglishTranslation = "to steal" });
            _list.Add(new Verb { id = 297, Infinitive = "vendre", EnglishTranslation = "to sell" });
            _list.Add(new Verb { id = 298, Infinitive = "venir", EnglishTranslation = "to come" });
            _list.Add(new Verb { id = 299, Infinitive = "verser", EnglishTranslation = "to pour" });
            _list.Add(new Verb { id = 300, Infinitive = "visiter", EnglishTranslation = "to visit" });
            _list.Add(new Verb { id = 301, Infinitive = "vivre", EnglishTranslation = "to live" });
            _list.Add(new Verb { id = 302, Infinitive = "voir", EnglishTranslation = "to see" });
            _list.Add(new Verb { id = 303, Infinitive = "voler", EnglishTranslation = "to steal / to fly" });
            _list.Add(new Verb { id = 304, Infinitive = "vomir", EnglishTranslation = "to vomit" });
            _list.Add(new Verb { id = 305, Infinitive = "vouloir", EnglishTranslation = "to want" });
            _list.Add(new Verb { id = 306, Infinitive = "zozoter", EnglishTranslation = "to lisp" });
            return _list;
        }



    }

}
