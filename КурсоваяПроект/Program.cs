using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Collections;

namespace КурсоваяПроект
{
    class user
    {
       public string first_name;
       public string second_name;
       public string third_name;
       public string password;
       public string role;
       public string status;
       public int id;
       protected String connectionString;
       protected NpgsqlConnection npgSqlConnection;
       protected NpgsqlDataReader npgSqlDataReader;

        public NpgsqlDataReader SELECT(string select) {

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand(select, npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            return npgSqlDataReader;
        }
        public void Autorization()
        {
            NpgsqlDataReader npgSqlDataReader = SELECT($"SELECT * FROM users WHERE (first_name='{first_name}' AND" +
                $" second_name='{second_name}' AND third_name='{third_name}' AND password='{password}')");

            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    id = (int)dbDataRecord["user_id"];
                    role = (string)dbDataRecord["role"];
                    status = (string)dbDataRecord["status"];
                }
            }
            else
            {
                role = "";
                status = "";
            }
            npgSqlDataReader.Close();
        }

        public void clear_user()
        {
          first_name="";
          second_name="";
          third_name="";
          password="";
          role="";
          status = "";
          id = 0;
          connectionString = "Server=localhost;Port=5433;Username=postgres;Password=12345;Database=course_work;";
          npgSqlConnection = new NpgsqlConnection(connectionString);
          npgSqlConnection.Open();
        }
    }

    class admin:user
    {
        public struct users
        {
          public string  first_name,
          second_name,
          third_name,
          password,
          role,
          status;
          public int id;
            public users(string first_name="", string second_name = "", string third_name = "", string password = "", string role = "", string status = "", int id=0)
            {
                this.first_name = first_name;
                this.second_name = second_name;
                this.third_name = third_name;
                this.password = password;
                this.role = role;
                this.status = status;
                this.id = id;
            }
        }
        public struct groups
        {
            public string name, year,status;
            public int id;
            public groups(string name="",string year="",string status="",int id=0)
            {
                this.name = name;
                this.year = year;
                this.status = status;
                this.id = id;
            }

        }
        public List<users> UsersList = new List<users>();
        public List<users> UsersList2 = new List<users>();
        public List<groups> GroupsList = new List<groups>();
        public admin(user user) 
        {
            id = user.id;
            connectionString = "Server=localhost;Port=5433;Username=postgres;Password=12345;Database=course_work;";
            npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
        }
        public void update_users_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT("SELECT * FROM users WHERE status='active' ORDER BY first_name,second_name, third_name");
            String UserDetails = "{0, -16}{1, -16}{2, -16}{3, -7}";
            listBox.Items.Clear();
            UsersList.Clear();
            listBox.Items.Add(String.Format(UserDetails, "Фамилия",
                    "Имя", "Отчество", "Роль"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                UsersList.Add(new users((String)dbDataRecord["first_name"], (String)dbDataRecord["second_name"], (String)dbDataRecord["third_name"],
                    (String)dbDataRecord["password"], (String)dbDataRecord["role"], (String)dbDataRecord["status"], (int)dbDataRecord["user_id"]));
            }
            foreach(users user in UsersList)
            {
                listBox.Items.Add(String.Format(UserDetails, user.first_name,
                    user.second_name, user.third_name, user.role));
            }
            npgSqlDataReader.Close();
        }

        public void update_groups_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT("SELECT name,status,group_id,EXTRACT(YEAR from year) from groups WHERE status='active'  ORDER BY name");
            String GroupDetails = "{0, -11}{1, -5}";
            listBox.Items.Clear();
            GroupsList.Clear();
            listBox.Items.Add(String.Format(GroupDetails, "Группа","Год"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                GroupsList.Add(new groups((String)dbDataRecord["name"], dbDataRecord["date_part"].ToString(), (String)dbDataRecord["status"],
                    (int)dbDataRecord["group_id"]));
            }
            foreach (groups group in GroupsList)
            {
                listBox.Items.Add(String.Format(GroupDetails, group.name, group.year));
            }
            npgSqlDataReader.Close();
        }

        public Boolean check_user(String first_name, String second_name, String third_name, String password, Boolean check)
        {
            if (first_name.Length > 0 && second_name.Length > 0 && third_name.Length > 0 && password.Length > 0)
            {
                if (check)
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM users WHERE (first_name='{first_name}'AND " +
                             $"second_name='{second_name}'AND third_name='{third_name}'AND password='{password}' AND user_id!='{id}')");
                }
                else
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM users WHERE (first_name='{first_name}'AND " +
                             $"second_name='{second_name}'AND third_name='{third_name}'AND password='{password}')");
                }
                if (npgSqlDataReader.HasRows)
                {
                    npgSqlDataReader.Close();
                    return false;
                }
                else
                {
                    npgSqlDataReader.Close();
                    return true;
                }

            }
            return false;
        }
        public void add_user(String first_name, String second_name, String third_name, String password, String role)
        {

            npgSqlDataReader = SELECT($"INSERT INTO users(first_name,second_name,third_name,password,role,status)" +
                   $" VALUES ('{first_name}','{second_name}','{third_name}','{password}','{role}','active')");

            npgSqlDataReader.Close();

        }

        public Boolean check_group(string name, string year, Boolean check)
        {
            int num;
            if (name.Length > 0 && int.TryParse(year, out num) && num > 999)
            {
                if (check)
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM groups WHERE(name='{name}' AND EXTRACT(YEAR from year)>='{year}' AND group_id!={id})");
                }
                else
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM groups WHERE(name='{name}' AND EXTRACT(YEAR from year)>='{year}')");
                }
                if (npgSqlDataReader.HasRows)
                {
                    npgSqlDataReader.Close();
                    return false;
                }
                else
                {
                    npgSqlDataReader.Close();
                    return true;
                }
            }
            return false;
        }
        public void add_group(string name, string year)
        {

            npgSqlDataReader = SELECT($"DO $do$ BEGIN IF EXISTS(SELECT * FROM groups WHERE name='{name}') THEN " +
                $"UPDATE groups SET status='deleted' WHERE (name='{name}'); END IF; " +
                $"INSERT INTO groups (name,year,status) VALUES ('{name}', '01/01/{year}','active');" +
                $" END $do$;");
            npgSqlDataReader.Close();
        }
        public void user_info(int index, TextBox first_name_, TextBox second_name_, TextBox third_name_, TextBox password_, CheckedListBox groups)
        {
            int j = 0;
            groups.Items.Clear();
            first_name_.Text = UsersList[index - 1].first_name;
            second_name_.Text = UsersList[index - 1].second_name;
            third_name_.Text = UsersList[index - 1].third_name;
            password_.Text = UsersList[index - 1].password;
            id = UsersList[index - 1].id;
            npgSqlDataReader = SELECT($"SELECT * from groups join users_groups on group_id=groups_group_id where users_user_id ={UsersList[index - 1].id} ORDER BY name DESC ");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (((string)dbDataRecord["status"]).Equals("active"))
                {
                    groups.Items.Insert(j, (string)dbDataRecord["name"]);
                    groups.SetItemChecked(j, true);
                }
            }
            npgSqlDataReader.Close();
            npgSqlDataReader = SELECT($"SELECT * from groups WHERE group_id NOT IN" +
                $"(SELECT group_id from groups join users_groups on group_id=groups_group_id where users_user_id ={UsersList[index - 1].id}) ORDER BY name");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (((string)dbDataRecord["status"]).Equals("active"))
                {
                    groups.Items.Add((string)dbDataRecord["name"]);
                }
            }
            npgSqlDataReader.Close();
        }

        public void group_info(int index, TextBox name_, TextBox year_, ListBox users)
        {
            String UserDetails = "{0, -16}{1, -16}{2, -16}{3, -7}";
            users.Items.Clear();
            UsersList.Clear();
            name_.Text = GroupsList[index - 1].name;
            year_.Text = GroupsList[index - 1].year;
            id = GroupsList[index - 1].id;
            users.Items.Add(String.Format(UserDetails, "Фамилия","Имя", "Отчество", "Роль"));
            npgSqlDataReader = SELECT($"SELECT * from users join users_groups on user_id=users_user_id" +
                $" where groups_group_id ={GroupsList[index - 1].id} ORDER BY first_name,second_name, third_name");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (((string)dbDataRecord["status"]).Equals("active"))
                {
                    UsersList.Add(new users((String)dbDataRecord["first_name"], (String)dbDataRecord["second_name"], (String)dbDataRecord["third_name"],
                   (String)dbDataRecord["password"], (String)dbDataRecord["role"], (String)dbDataRecord["status"], (int)dbDataRecord["user_id"]));
                }
            }
            foreach (users user in UsersList)
            {
                users.Items.Add(String.Format(UserDetails, user.first_name,
                    user.second_name, user.third_name, user.role));
            }
            npgSqlDataReader.Close();
        }
        public void group_info_add(CheckedListBox userslistbox)
        {
            String UserDetails = "{0, -16}{1, -16}{2, -16}{3, -7}";
            Boolean check = true;
            UsersList2.Clear();
            userslistbox.Items.Clear();
            npgSqlDataReader = SELECT($"SELECT * from users WHERE status='active' ORDER BY first_name,second_name, third_name");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                check = true;
                foreach (users user in UsersList)
                {
                    if (user.id == (int)dbDataRecord["user_id"])
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    UsersList2.Add(new users((String)dbDataRecord["first_name"], (String)dbDataRecord["second_name"], (String)dbDataRecord["third_name"],
                     (String)dbDataRecord["password"], (String)dbDataRecord["role"], (String)dbDataRecord["status"], (int)dbDataRecord["user_id"]));
                }
            }
            foreach (users user in UsersList2)
            {
                userslistbox.Items.Add(String.Format(UserDetails, user.first_name,
                    user.second_name, user.third_name, user.role));
            }
            npgSqlDataReader.Close();
        }
        public void group_info_add_done(CheckedListBox users_out , ListBox users_in)
        {
            int i = 0 ;
            String UserDetails = "{0, -16}{1, -16}{2, -16}{3, -7}";
            foreach (var user in users_out.Items)
            {
                if (users_out.GetItemCheckState(i) == CheckState.Checked)
                {
                    UsersList.Add(UsersList2[i]);
                    users_in.Items.Add((String.Format(UserDetails, UsersList2[i].first_name,
                    UsersList2[i].second_name, UsersList2[i].third_name, UsersList2[i].role)));
                }
                i++;
            }
        }
        public void group_info_delete_done(int index, ListBox users_in)
        {
            users_in.Items.RemoveAt(index);
            UsersList.RemoveAt(index-1);
        }
        public void edit_user(String first_name, String second_name, String third_name, String password, CheckedListBox groups)
        {
            npgSqlDataReader = SELECT($"UPDATE users SET first_name = '{first_name}',second_name = '{second_name}' ,third_name = '{third_name}' ,password = '{password}' " +
                $"WHERE (user_id={id})");
            npgSqlDataReader.Close();
            int i = 0;
            int group_id=0;
            foreach (var group in groups.Items )
            {
                npgSqlDataReader = SELECT($"SELECT * FROM groups WHERE (name='{group}' AND status='active')");
                if (npgSqlDataReader.HasRows)
                {
                    if (groups.GetItemCheckState(i) == CheckState.Checked)
                    {
                        foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                        {
                            group_id = (int)dbDataRecord["group_id"];
                        }
                        npgSqlDataReader.Close();
                        npgSqlDataReader = SELECT($"DO $do$ BEGIN IF NOT EXISTS (SELECT * FROM users_groups WHERE (users_user_id={id} AND groups_group_id={group_id})) THEN " +
                                $"INSERT INTO users_groups(users_user_id,groups_group_id) VALUES({id},{group_id}); END IF; END $do$;");
                        npgSqlDataReader.Close();
                    }

                    if (groups.GetItemCheckState(i) == CheckState.Unchecked)
                    {
                        foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                        {
                            group_id = (int)dbDataRecord["group_id"];
                        }
                        npgSqlDataReader.Close();
                        npgSqlDataReader = SELECT($"DO $do$ BEGIN IF EXISTS (SELECT * FROM users_groups WHERE (users_user_id={id} AND groups_group_id={group_id})) THEN " +
                                $"DELETE FROM users_groups WHERE (users_user_id={id} AND groups_group_id={group_id}); END IF; END $do$;");
                        npgSqlDataReader.Close();
                    }
                }
                i++;
            }
        }

        public void edit_group(String name, String year)
        {
            npgSqlDataReader = SELECT($"UPDATE groups SET name = '{name}',year = '01/01/{year}' " +
                $"WHERE (group_id={id})");
            npgSqlDataReader.Close();
            foreach (var user in UsersList)
            {
                npgSqlDataReader = SELECT($"DO $do$ BEGIN IF NOT EXISTS (SELECT * FROM users_groups WHERE (users_user_id={user.id} AND groups_group_id={id})) THEN " +
                        $"INSERT INTO users_groups(users_user_id,groups_group_id) VALUES({user.id},{id}); END IF; END $do$;");
                npgSqlDataReader.Close();
            }

            Boolean check = true;
            UsersList2.Clear();
            npgSqlDataReader = SELECT($"SELECT * from users WHERE status='active' ORDER BY first_name,second_name, third_name");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                check = true;
                foreach (users user in UsersList)
                {
                    if (user.id == (int)dbDataRecord["user_id"])
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    UsersList2.Add(new users((String)dbDataRecord["first_name"], (String)dbDataRecord["second_name"], (String)dbDataRecord["third_name"],
                     (String)dbDataRecord["password"], (String)dbDataRecord["role"], (String)dbDataRecord["status"], (int)dbDataRecord["user_id"]));
                }
            }
            npgSqlDataReader.Close();

            foreach (var user in UsersList2)
            {
                npgSqlDataReader = SELECT($"DO $do$ BEGIN IF EXISTS (SELECT * FROM users_groups WHERE (users_user_id={user.id} AND groups_group_id={id})) THEN " +
                        $"DELETE FROM users_groups WHERE (users_user_id={user.id}  AND groups_group_id= {id}); END IF; END $do$;");
                npgSqlDataReader.Close();
            }
        }
        public void delete_user()
        {
            npgSqlDataReader = SELECT($"UPDATE users SET status = 'deleted' " +
                $"WHERE (user_id={id})");
            npgSqlDataReader.Close();
        }
        public void delete_group()
        {
            npgSqlDataReader = SELECT($"UPDATE groups SET status = 'deleted' " +
    $"WHERE (group_id={id})");
            npgSqlDataReader.Close();
        }
    }

    public struct word_pair
    {
        public string word, translation, status;
        public int id;
        public word_pair(string word = "", string translation = "", string status = "", int id = 0)
        {
            this.word = word;
            this.translation = translation;
            this.status = status;
            this.id = id;
        }

    }

    class teacher : user 
    {
        public int word_id;
        public string module_name;
        public struct module
        {
            public string name, status;
            public int id;
            public module(string name = "", string status = "", int id = 0)
            {
                this.name = name;
                this.status = status;
                this.id = id;
            }

        }

        public List<word_pair> WordsList = new List<word_pair>();
        public List<word_pair> WordsList2 = new List<word_pair>();
        public List<module> ModulesList = new List<module>();

        public teacher(user user)
        {
            id = user.id;
            connectionString = "Server=localhost;Port=5433;Username=postgres;Password=12345;Database=course_work;";
            npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
        }

        public void update_words_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT("SELECT * FROM word_pairs WHERE (status='active' AND reliability=true) ORDER BY word");
            String WordDetails = "{0, -21}{1, -21}";
            listBox.Items.Clear();
            WordsList.Clear();
            listBox.Items.Add(String.Format(WordDetails, "WORD", "TRANSLATION"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                WordsList.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], (String)dbDataRecord["status"], (int)dbDataRecord["pair_id"]));
            }
            foreach (word_pair words in WordsList)
            {
                listBox.Items.Add(String.Format(WordDetails, words.word, words.translation));
            }
            npgSqlDataReader.Close();
        }

        public void update_modules_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT($"SELECT * FROM modules WHERE (status='active' AND teacher_id={id}) ORDER BY name");
            String ModuleDetails = "{0, -41}";
            listBox.Items.Clear();
            ModulesList.Clear();
            listBox.Items.Add(String.Format(ModuleDetails, "Название модуля"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                ModulesList.Add(new module((String)dbDataRecord["name"], (String)dbDataRecord["status"], (int)dbDataRecord["teacher_id"]));
            }
            foreach (module modules in ModulesList)
            {
                listBox.Items.Add(String.Format(ModuleDetails, modules.name));
            }
            npgSqlDataReader.Close();
        }

        public Boolean check_word(string word, string translation, Boolean check)
        {
            if (word.Length > 0 && translation.Length > 0)
            {
                if (check)
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM word_pairs WHERE(word='{word}' AND translation='{translation}' AND reliability=true AND pair_id!={word_id})");
                }
                else
                {
                    npgSqlDataReader = SELECT($"SELECT * FROM word_pairs WHERE(word='{word}' AND reliability=true AND translation='{translation}')");
                }
                if (npgSqlDataReader.HasRows)
                {
                    npgSqlDataReader.Close();
                    return false;
                }
                else
                {
                    npgSqlDataReader.Close();
                    return true;
                }
            }
            return false;
        }

        public Boolean check_module(string name,  Boolean check)
        {
            if (name.Length > 0)
            {
                if (check)
                {
                    if (module_name.Equals(name))
                    {
                        return true;
                    }
                    else
                    {
                        npgSqlDataReader = SELECT($"SELECT * FROM modules WHERE(name='{name}')");
                    }
                }
                else
                {
                        npgSqlDataReader = SELECT($"SELECT * FROM modules WHERE(name='{name}')");
                }
                if (npgSqlDataReader.HasRows)
                {
                    npgSqlDataReader.Close();
                    return false;
                }
                else
                {
                    npgSqlDataReader.Close();
                    return true;
                }
            }
            return false;
        }

        public void add_word(string word, string translation)
        {

            npgSqlDataReader = SELECT($"INSERT INTO word_pairs(word,translation,reliability,status)" +
                   $" VALUES ('{word}','{translation}', true,'active')");

            npgSqlDataReader.Close();
        }

        public void add_module(string name)
        {

            npgSqlDataReader = SELECT($"INSERT INTO modules(name,teacher_id,status)" +
                   $" VALUES ('{name}','{id}','active')");

            npgSqlDataReader.Close();
        }

        public void word_info(int index, TextBox word, TextBox translation)
        {
            word.Text = WordsList[index - 1].word;
            translation.Text = WordsList[index - 1].translation;
            word_id = WordsList[index - 1].id;
        }

        public void module_info(int index, TextBox name, ListBox words_list)
        {
            name.Text = ModulesList[index - 1].name;
            module_name = ModulesList[index - 1].name;

            npgSqlDataReader = SELECT($"SELECT * FROM word_pairs JOIN modules_word_pairs ON word_pairs_pair_id=pair_id" +
                $" WHERE modules_name ='{module_name}' ORDER BY modules_name");
            String WordDetails = "{0, -21}{1, -21}";
            words_list.Items.Clear();
            WordsList.Clear();
            words_list.Items.Add(String.Format(WordDetails, "WORD", "TRANSLATION"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                WordsList.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], (String)dbDataRecord["status"], (int)dbDataRecord["pair_id"]));
            }
            foreach (word_pair words in WordsList)
            {
                words_list.Items.Add(String.Format(WordDetails, words.word, words.translation));
            }
            npgSqlDataReader.Close();
        }

        public void module_info_add(CheckedListBox words_listbox)
        {
            String WordDetails = "{0, -21}{1, -21}";
            Boolean check = true;
            WordsList2.Clear();
            words_listbox.Items.Clear();
            npgSqlDataReader = SELECT($"SELECT * from word_pairs WHERE status='active' AND reliability=true ORDER BY word");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                check = true;
                foreach (word_pair words in WordsList)
                {
                    if (words.id == (int)dbDataRecord["pair_id"])
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    WordsList2.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], (String)dbDataRecord["status"], (int)dbDataRecord["pair_id"]));
                }
            }
            foreach (word_pair words in WordsList2)
            {
                words_listbox.Items.Add(String.Format(WordDetails, words.word, words.translation));
            }
            npgSqlDataReader.Close();
        }

        public void module_info_add_done(CheckedListBox words_out, ListBox words_in)
        {
            int i = 0;
            String WordDetails = "{0, -21}{1, -21}";
            foreach (var word in words_out.Items)
            {
                if (words_out.GetItemCheckState(i) == CheckState.Checked)
                {
                    WordsList.Add(WordsList2[i]);
                    words_in.Items.Add((String.Format(WordDetails, WordsList2[i].word, WordsList2[i].translation)));
                }
                i++;
            }
        }

        public void module_info_delete_done(int index, ListBox words_in)
        {
            words_in.Items.RemoveAt(index);
            WordsList.RemoveAt(index - 1);
        }

        public void edit_word(String word, String translation)
        {
            npgSqlDataReader = SELECT($"UPDATE word_pairs SET word = '{word}',translation = '{translation}' WHERE (pair_id={word_id})");
            npgSqlDataReader.Close();
        }

        public void edit_module(String name)
        {
            npgSqlDataReader = SELECT($"UPDATE modules SET name = '{name}' " +
                $"WHERE (name='{module_name}')");
            npgSqlDataReader.Close();
            foreach (var word in WordsList)
            {
                npgSqlDataReader = SELECT($"DO $do$ BEGIN IF NOT EXISTS (SELECT * FROM modules_word_pairs WHERE (word_pairs_pair_id={word.id} AND modules_name='{name}')) THEN " +
                        $"INSERT INTO modules_word_pairs(word_pairs_pair_id,modules_name) VALUES({word.id},'{name}'); END IF; END $do$;");
                npgSqlDataReader.Close();
            }

            Boolean check = true;
            WordsList2.Clear();
            npgSqlDataReader = SELECT($"SELECT * from word_pairs WHERE status='active' ORDER BY word");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                check = true;
                foreach (word_pair words in WordsList)
                {
                    if (words.id == (int)dbDataRecord["pair_id"])
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    WordsList2.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], (String)dbDataRecord["status"], (int)dbDataRecord["pair_id"]));
                }
            }
            npgSqlDataReader.Close();

            foreach (var word in WordsList2)
            {
                npgSqlDataReader = SELECT($"DO $do$ BEGIN IF EXISTS (SELECT * FROM modules_word_pairs WHERE (word_pairs_pair_id={word.id} AND modules_name='{name}')) THEN " +
                        $"DELETE FROM modules_word_pairs WHERE (word_pairs_pair_id={word.id} AND modules_name='{name}'); END IF; END $do$;");
                npgSqlDataReader.Close();
            }
        }

        public void delete_word()
        {
            npgSqlDataReader = SELECT($"UPDATE word_pairs SET status = 'deleted' " +
                $"WHERE (pair_id={word_id})");
            npgSqlDataReader.Close();
        }

        public void delete_module()
        {
            npgSqlDataReader = SELECT($"UPDATE modules SET status = 'deleted' " +
                $"WHERE (name='{module_name}')");
            npgSqlDataReader.Close();
        }

        public void statistics(string stat_type, ListBox listBox)
        {
            switch (stat_type)
            {
                case "группам":
                    npgSqlDataReader = SELECT($"select   modules.name as module_name,groups.name , count(users.user_id) as now, " +
                        $"(SELECT count(users_user_id) from users_groups WHERE  users_user_id in (SELECT user_id FROM users WHERE status = 'active' and role = 'student') AND groups_group_id in (SELECT groups_group_id FROM users_groups WHERE users_user_id = teacher_id)) from " +
                        $"(SELECT distinct user_id, module_name, status from subscribes) as sub " +
                        $" JOIN modules on module_name = modules.name " +
                        $" JOIN users on users.user_id = sub.user_id " +
                        $" JOIN users_groups on users.user_id = users_user_id " +
                        $"JOIN groups on groups_group_id = group_id " +
                        $"WHERE teacher_id = {id} AND groups.status = 'active' and users.status = 'active'and users.role = 'student' and modules.status = 'active' " +
                        $"AND group_id in(SELECT groups_group_id FROM users_groups WHERE users_user_id = teacher_id) " +
                        $"group by modules.name, groups.name");
                    String StatDetails1 = "{0, -21}{1, -11}{2, -11}{3, -11}";
                    listBox.Items.Clear();
                    listBox.Items.Add(String.Format(StatDetails1, "МОДУЛЬ","ГРУППА", "ПРИСТУПИЛО","ВСЕГО"));
                    foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                    {
                        listBox.Items.Add(String.Format(StatDetails1, (string)dbDataRecord["module_name"], (string)dbDataRecord["name"], dbDataRecord["now"].ToString(), dbDataRecord["count"].ToString() ));
                    }
                    npgSqlDataReader.Close();
                    break;
                case "студентам":
                    npgSqlDataReader = SELECT($"select  modules.name as module_name, first_name,second_name,third_name, sum(case when result>0 then result else 0 end), count(result), date from checks " +
                        $"JOIN subscribes on checks.subscribe_id = subscribes.subscribe_id " +
                        $"JOIN modules on module_name = modules.name " +
                        $"JOIN users on users.user_id = subscribes.user_id " +
                        $"JOIN users_groups on users.user_id = users_user_id " +
                        $"JOIN groups on groups_group_id = group_id " +
                        $"WHERE teacher_id = {id} AND " +
                        $"groups.status = 'active' and users.status = 'active' and modules.status = 'active'" +
                        $"AND group_id in(SELECT groups_group_id FROM users_groups WHERE users_user_id=teacher_id)" +
                        $" group by modules.name,users.user_id, date");
                    String StatDetails2 = "{0, -21}{1, -16}{2, -16}{3, -16}{4, -11}{5, -11}{6, -15}";
                    listBox.Items.Clear();
                    listBox.Items.Add(String.Format(StatDetails2, "МОДУЛЬ", "ФАМИЛИЯ", "ИМЯ", "ОТЧЕСТВО", "УСПЕШНЫХ", "ВСЕГО","ДАТА"));
                    foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                    {
                        listBox.Items.Add(String.Format(StatDetails2, (string)dbDataRecord["module_name"], (string)dbDataRecord["first_name"], (string)dbDataRecord["second_name"], (string)dbDataRecord["third_name"], dbDataRecord["sum"].ToString(), dbDataRecord["count"].ToString(), ((DateTime)dbDataRecord["date"]).ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    npgSqlDataReader.Close();
                    break;
                case "модулям":
                    npgSqlDataReader = SELECT($"with t as (" +
                        $"select  modules.name as module_name, first_name, second_name, third_name, sum(case when result> 0 then result else 0 end) as s, count(result) as c, date from checks " +
                        $"JOIN subscribes on checks.subscribe_id = subscribes.subscribe_id "  +
                        $"JOIN modules on module_name = modules.name " +
                        $"JOIN users on users.user_id = subscribes.user_id " +
                        $"JOIN users_groups on users.user_id = users_user_id " +
                        $"JOIN groups on groups_group_id = group_id " +
                        $"WHERE teacher_id = {id} AND " +
                        $"groups.status = 'active' and users.status = 'active' and modules.status = 'active' " +
                        $"AND group_id in(SELECT groups_group_id FROM users_groups WHERE users_user_id = teacher_id) " +
                        $"group by modules.name,users.user_id, date) " +
                        $"select module_name, avg(s) as now ,avg(c) FROM t group by module_name");
                    String StatDetails3 = "{0, -21}{1,-31}{2,-31}";
                    listBox.Items.Clear();
                    listBox.Items.Add(String.Format(StatDetails3, "МОДУЛЬ", "СРЕДНЕЕ УСПЕШНЫХ РЕШЕНИЙ", "СРЕДНЕЕ ВСЕГО РЕШЕНИЙ"));
                    foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                    {
                        listBox.Items.Add(String.Format(StatDetails3, (string)dbDataRecord["module_name"],  dbDataRecord["now"].ToString(), dbDataRecord["avg"].ToString()));
                    }
                    npgSqlDataReader.Close();
                    break;
                default:
                    break;
            }
        }
    }

    class student : user
    {
        public DateTime dateTime;
        public List<string> ModulesList = new List<string>();
        public List<word_pair> WordsList = new List<word_pair>();

        public struct check
        {
            public string word, translation;
            public int word_id, subscribe_id;
            public check(string word = "", string translation = "",  int word_id = 0, int subscribe_id = 0)
            {
                this.word = word;
                this.translation = translation;
                this.word_id = word_id;
                this.subscribe_id = subscribe_id;
            }

        }

        public List<check> ChecksList = new List<check>();
        public student(user user)
        {
            id = user.id;
            connectionString = "Server=localhost;Port=5433;Username=postgres;Password=12345;Database=course_work;";
            npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
        }

        public void update_words_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT($"SELECT *," +
                $"(SELECT true WHERE EXISTS(SELECT * from subscribes WHERE (user_id = {id} AND subscribes.pair_id = word_pairs.pair_id AND subscribes.status = 'active')))" +
                $"FROM word_pairs WHERE(status = 'active' AND reliability = true) ORDER BY word");
            String WordDetails = "{0, -21}{1, -21}{2,-11}";
            listBox.Items.Clear();
            WordsList.Clear();
            listBox.Items.Add(String.Format(WordDetails, "WORD", "TRANSLATION","STATUS"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if(!dbDataRecord.IsDBNull(dbDataRecord.GetOrdinal("bool")))
                {
                    WordsList.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], "ИЗУЧАЮ", (int)dbDataRecord["pair_id"]));
                }
                else
                {
                    WordsList.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], "", (int)dbDataRecord["pair_id"]));
                }

            }
            foreach (word_pair words in WordsList)
            {
                listBox.Items.Add(String.Format(WordDetails, words.word, words.translation, words.status));
            }
            npgSqlDataReader.Close();
        }

        public void update_mywords_table(ListBox listBox)
        {
            npgSqlDataReader = SELECT($"SELECT * FROM word_pairs WHERE" +
                $" EXISTS(SELECT * from subscribes WHERE (user_id = {id} AND subscribes.pair_id = word_pairs.pair_id AND subscribes.status = 'active'))" +
                $" AND (status = 'active') ORDER BY word");
            String WordDetails = "{0, -21}{1, -21}";
            listBox.Items.Clear();
            WordsList.Clear();
            listBox.Items.Add(String.Format(WordDetails, "WORD", "TRANSLATION"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                    WordsList.Add(new word_pair((String)dbDataRecord["word"], (String)dbDataRecord["translation"], "ИЗУЧАЮ", (int)dbDataRecord["pair_id"]));
            }
            foreach (word_pair words in WordsList)
            {
                listBox.Items.Add(String.Format(WordDetails, words.word, words.translation));
            }
            npgSqlDataReader.Close();
        }
        public void subscribe(int index)
        {
            npgSqlDataReader = SELECT($"DO $do$" +
                $" BEGIN IF NOT EXISTS(SELECT* FROM subscribes WHERE (user_id= {id} AND pair_id = {WordsList[index-1].id} AND module_name IS NULL)) THEN " +
                $"INSERT INTO subscribes(user_id, pair_id, status)VALUES({id}, {WordsList[index - 1].id}, 'active');END IF; " +
                $"IF EXISTS(SELECT* FROM subscribes WHERE (user_id= {id} AND pair_id = {WordsList[index - 1].id}  AND status = 'deleted')) THEN " +
                $"UPDATE subscribes SET status = 'active' WHERE(user_id = {id} AND pair_id = {WordsList[index - 1].id}); END IF; END $do$; ");
            npgSqlDataReader.Close();
        }

        public Boolean check_word(string word, string translation)
        {
            if (word.Length > 0 && translation.Length > 0)
            {
                npgSqlDataReader = SELECT($"SELECT * FROM word_pairs WHERE(word='{word}' AND reliability=true AND translation='{translation}')");

                if (npgSqlDataReader.HasRows)
                {
                    npgSqlDataReader.Close();
                    return false;
                }
                else
                {
                    npgSqlDataReader.Close();
                    return true;
                }
            }
            return false;
        }
        public void add_word(string word, string translation)
        {
            int pair_id=0;
            npgSqlDataReader = SELECT($"INSERT INTO word_pairs(word,translation,reliability,status)" +
                   $" VALUES ('{word}','{translation}', false,'active') RETURNING pair_id");
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                pair_id = (int)dbDataRecord["pair_id"];
            }
            npgSqlDataReader.Close();

            npgSqlDataReader = SELECT($"INSERT INTO subscribes(user_id, pair_id, status)VALUES({id}, {pair_id}, 'active')");
            npgSqlDataReader.Close();
        }

        public void unsubscribe(int index)
        {
            npgSqlDataReader = SELECT($"DO $do$" +
    $" BEGIN DELETE from subscribes where (pair_id={WordsList[index-1].id} AND user_id={id} AND module_name IS NULL);" +
    $" UPDATE subscribes SET status='deleted' where (pair_id={WordsList[index - 1].id} AND user_id={id} AND module_name IS NOT NULL); " +
    $" DELETE FROM word_pairs WHERE (reliability=false AND NOT EXISTS(SELECT * FROM subscribes WHERE subscribes.pair_id=word_pairs.pair_id)); END $do$; ");
            npgSqlDataReader.Close();
        }

        public void update_mymodules_table(ListBox listBox)
        {
            
            npgSqlDataReader = SELECT($"SELECT name FROM modules WHERE status='active' AND modules.name IN " +
                $"(SELECT name FROM modules WHERE teacher_id IN " +
                $"(SELECT user_id FROM users WHERE role = 'teacher' and status = 'active' and user_id IN " +
                $"(SELECT users_user_id FROM users_groups WHERE EXISTS(SELECT * FROM groups WHERE groups_group_id = group_id AND status = 'active')" +
                $" AND groups_group_id IN (SELECT groups_group_id from users_groups WHERE users_user_id = {id}))))");
            String ModuleDetails = "{0, -41}";
            listBox.Items.Clear();
            ModulesList.Clear();
            listBox.Items.Add(String.Format(ModuleDetails, "НАЗВАНИЕ"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                ModulesList.Add( (string)dbDataRecord["name"]);
            }
            foreach (var modules in ModulesList)
            {
                listBox.Items.Add(String.Format(ModuleDetails, modules));
            }
            npgSqlDataReader.Close();
        }

        public void training()
        {
            dateTime = DateTime.Now;
            npgSqlDataReader = SELECT($"SELECT word_pairs.pair_id,word,translation,subscribes.subscribe_id," +
    $" (SELECT sum(result) FROM checks WHERE subscribe_id = subscribes.subscribe_id) as k FROM word_pairs" +
    $" JOIN subscribes on subscribes.pair_id = word_pairs.pair_id" +
    $" LEFT OUTER JOIN  checks on checks.subscribe_id = subscribes.subscribe_id" +
    $" WHERE(user_id = {id}  AND subscribes.status = 'active' AND module_name IS NULL" +
    $" AND word_pairs.status = 'active') GROUP BY word_pairs.pair_id, subscribes.subscribe_id order by k");
            ChecksList.Clear();
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {

                ChecksList.Add(new check((string)dbDataRecord["word"], (string)dbDataRecord["translation"], (int)dbDataRecord["pair_id"], (int)dbDataRecord["subscribe_id"]));


            }

            npgSqlDataReader.Close();
        }

        public bool check_list()
        {
            if (ChecksList.Count == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        public void next_stage(Label word_label, Label translation_label)
        {
            word_label.Text = ChecksList[0].word;
            translation_label.Text = ChecksList[0].translation;
        }

        public void result(bool res)
        {
            if (res)
            {
                npgSqlDataReader = SELECT($"INSERT INTO checks(subscribe_id,date,result) VALUES({ChecksList[0].subscribe_id},TIMESTAMP '{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}',1) ");
                npgSqlDataReader.Close();
                ChecksList.RemoveAt(0);
            }
            else
            {
                npgSqlDataReader = SELECT($"INSERT INTO checks(subscribe_id,date,result) VALUES({ChecksList[0].subscribe_id},TIMESTAMP'{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}',-1) ");
                npgSqlDataReader.Close();
                ChecksList.RemoveAt(0);
            }
        }

        public void start_module(int index)
        {
            dateTime = DateTime.Now;
            npgSqlDataReader = SELECT($"SELECT * FROM word_pairs WHERE status='active' AND pair_id IN" +
                $"(SELECT word_pairs_pair_id FROM modules_word_pairs WHERE modules_name = '{ModulesList[index-1]}') ");
            ChecksList.Clear();
            WordsList.Clear();
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {

                WordsList.Add(new word_pair((string)dbDataRecord["word"], (string)dbDataRecord["translation"],"", (int)dbDataRecord["pair_id"]));
            }
            npgSqlDataReader.Close();
            foreach (word_pair word in WordsList)
            {
                npgSqlDataReader = SELECT($"DO $do$ BEGIN IF NOT EXISTS (SELECT * FROM subscribes WHERE (user_id={id} AND pair_id={word.id} AND module_name='{ModulesList[index - 1]}')) THEN " +
                        $"INSERT INTO subscribes(user_id, pair_id, status, module_name)VALUES({id}, {word.id}, 'active','{ModulesList[index - 1]}'); END IF; " +
                        $"IF NOT EXISTS (SELECT * FROM subscribes WHERE (user_id={id} AND pair_id={word.id} AND module_name IS NULL)) THEN " +
                        $"INSERT INTO subscribes(user_id, pair_id, status)VALUES({id}, {word.id}, 'active'); END IF; END $do$; ");
                npgSqlDataReader.Close();

                npgSqlDataReader = SELECT($"SELECT * FROM subscribes WHERE (user_id={id} AND pair_id={word.id} AND module_name='{ModulesList[index - 1]}')");
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    ChecksList.Add(new check(word.word, word.translation, word.id, (int)dbDataRecord["subscribe_id"]));
                }
                npgSqlDataReader.Close();
            }
        }

        public void update_module_words_table(ListBox listBox)
        {
            String WordDetails = "{0, -21}{1, -21}";
            listBox.Items.Clear();
            listBox.Items.Add(String.Format(WordDetails, "WORD", "TRANSLATION"));
            foreach (word_pair words in WordsList)
            {
                listBox.Items.Add(String.Format(WordDetails, words.word, words.translation));
            }
        }
        
        public void statistics(ListBox listBox)
        {
            npgSqlDataReader = SELECT($"select  word, translation, sum(case when result>0 then result else 0 end), count(result) from checks " +
                $"JOIN subscribes on subscribes.subscribe_id = checks.subscribe_id " +
                $"JOIN word_pairs on word_pairs.pair_id = subscribes.pair_id " +
                $"WHERE user_id = {id} AND word_pairs.status = 'active' AND (subscribes.status = 'active' OR module_name IS NOT NULL) group by word, translation");
            String StatDetails3 = "{0, -21}{1,-21}{2,-21}{3,-21}";
            listBox.Items.Clear();
            listBox.Items.Add(String.Format(StatDetails3, "СЛОВО", "ПЕРЕВОД", "УСПЕШНЫХ РЕШЕНИЙ", "ВСЕГО РЕШЕНИЙ"));
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                listBox.Items.Add(String.Format(StatDetails3, (string)dbDataRecord["word"], (string)dbDataRecord["translation"], dbDataRecord["sum"].ToString(), dbDataRecord["count"].ToString()));
            }
            npgSqlDataReader.Close();
        }
    }

    static class Program
    {
        public static Form1 Autorization;
        public static Form2 Admin;
        public static Form3 Teacher;
        public static Form4 Student;
        public static Form5 UsersTable;
        public static Form6 Create_user;
        public static Form7 Edit_user;
        public static Form8 GroupsTable;
        public static Form9 Create_group;
        public static Form10 Edit_group;
        public static Form11 Edit_group_add_info;
        public static Form12 WordsTable;
        public static Form13 Create_word;
        public static Form14 Edit_word;
        public static Form15 ModulesTable;
        public static Form16 Create_module;
        public static Form17 Edit_module;
        public static Form18 Edit_module_add_info;
        public static Form19 Statistics;
        public static Form20 Dictionary;
        public static Form21 MyDictionary;
        public static Form22 Create_word_MyDictionary;
        public static Form23 ModulesList;
        public static Form24 Training;
        public static Form25 ModulesWords;
        public static Form26 Student_Statistics;
        public static user user = new user();
        public static admin admin;
        public static teacher teacher;
        public static student student;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Admin = new Form2();
            Teacher = new Form3();
            Student = new Form4();
            UsersTable = new Form5();
            Create_user = new Form6();
            Edit_user = new Form7();
            GroupsTable = new Form8();
            Create_group = new Form9();
            Edit_group = new Form10();
            Edit_group_add_info = new Form11();
            WordsTable = new Form12();
            Create_word = new Form13();
            Edit_word = new Form14();
            ModulesTable = new Form15();
            Create_module = new Form16();
            Edit_module = new Form17();
            Edit_module_add_info = new Form18();
            Statistics = new Form19();
            Dictionary = new Form20();
            MyDictionary = new Form21();
            Create_word_MyDictionary = new Form22();
            ModulesList = new Form23();
            Training = new Form24();
            ModulesWords = new Form25();
            Student_Statistics = new Form26();
            Application.Run(Autorization= new Form1());
        }
    }
}
