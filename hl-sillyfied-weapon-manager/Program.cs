// See https://aka.ms/new-console-template for more informaTION

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

class WeaponManager
{
    //declare vars
    static string m_strPath = @"";
    static string m_strName = "Code.txt";
    static string m_strFullPath;
    static string m_strVersion = "A.2.050124";
    static string m_strInternalWeaponName;
    static string m_strInternalWeaponNormal;
    static string m_strInternalWeaponLower;//lowercase
    static string m_strInternalWeaponUpper;//uppercase

    public WeaponManager()
    {
        m_strFullPath = Path.Combine(m_strPath, m_strName);
    }

    static void Main()
    {

        string m_strMainMenuChoice = "";

        while (true) {
            
            Console.WriteLine("Half-Life Sillyfied Weapon Manager \nVersion " + m_strVersion);
            Console.WriteLine("Type \'setup\' to setup the code or type \'path\' to change output path.");
            m_strMainMenuChoice = Console.ReadLine(); 

            switch (m_strMainMenuChoice)
            {
                case "setup":
                    WeaponCodeSettings(); //go to 
                    break;
                case "path":
                    FilePathChanger();  //go to
                    break;   

                default: Console.WriteLine("Another input has been detected, please try again...");
                    break;

            }

           
        }
    }

    public static void MakeStringUppercase()
    {
        // check for empty string.
        if (string.IsNullOrEmpty(m_strInternalWeaponName))
        {
            
        }
        // give thing
        m_strInternalWeaponNormal = char.ToUpper(m_strInternalWeaponName[0]) + m_strInternalWeaponName.Substring(1);
    }


    public static void WeaponCodeSettings()
    {
        int m_iSettingCycler = 0;
        //internal weapon name
        
        
        int m_iMagSize = 0;//mag size
        string m_strCNDOption; //are we going to use CND?
        string m_strEjectOption; //what case is the weapon going to eject?
        string m_strCritOption; // is the weapon going to use crits?


        while (true)
        {
            switch (m_iSettingCycler)
            {
                case 0:
                    Console.WriteLine("Initializating the code...");

                    //write starting code
                    using (StreamWriter sw = File.AppendText(m_strFullPath))
                    {
                        sw.WriteLine("");
                        sw.WriteLine("=========== START CODE, DO NOT COPY THIS LINE! ==========");
                        sw.WriteLine("// Half-Life: Sillyfied weapon file");
                        sw.WriteLine("// Code is generated with Half-Life: Sillyfied Weapon Manager version " + m_strVersion);
                        sw.WriteLine("#include \"extdll.h\"");
                        sw.WriteLine("#include \"util.h\"");
                        sw.WriteLine("#include \"cbase.h\"");
                        sw.WriteLine("#include \"monsters.h\"");
                        sw.WriteLine("#include \"weapons.h\"");
                        sw.WriteLine("#include \"player.h\"");
                        sw.WriteLine("#include \"soundent.h\"");
                        sw.WriteLine("#include \"gamerules.h\"");
                        sw.WriteLine("#include \"UserMessages.h\"");
                        sw.WriteLine(); //line skip
                    }
                    m_iSettingCycler++;
                    break;
                case 1:
                    
                    Console.WriteLine("*SETUP AND SPAWN FUNCTIONS*");
                    Console.WriteLine("Insert internal weapon name: (used for console commands and entity names)");
                    m_strInternalWeaponName = Console.ReadLine(); //ask user for the internal weapon name

                    //make changes to the input

                    m_strInternalWeaponLower = m_strInternalWeaponName.ToLower();
                    m_strInternalWeaponUpper = m_strInternalWeaponName.ToUpper();
                    MakeStringUppercase();

                    //add code till we hit to a request
                    using (StreamWriter sw = File.AppendText(m_strFullPath))
                    {
                        sw.WriteLine("LINK_ENTITY_TO_CLASS(weapon_" +m_strInternalWeaponLower+ ", C"+m_strInternalWeaponNormal+");\r\n"); //LINK_ENTITY_TO_CLASS(weapon_ak47, CAk47);
                        sw.WriteLine(""); //line skip
                        sw.WriteLine("void C" + m_strInternalWeaponNormal + "::Spawn()");
                        sw.WriteLine("{");
                        sw.WriteLine("Precache();");
                        sw.WriteLine("SET_MODEL(ENT(pev), \"models/w_" + m_strInternalWeaponLower + ".mdl\");");
                        sw.WriteLine("m_iId = WEAPON_" + m_strInternalWeaponUpper + ";");
                    } // stop writing here, code asks for clip size
                    m_iSettingCycler++;
                    break;
                case 2:
                    Console.WriteLine("*SPAWN FUNCTION*");
                    Console.WriteLine("What is the magazine/clip size of this weapon? (answer in intergers)");
                    m_iMagSize = Convert.ToInt32(Console.ReadLine());
                    // m_iDefaultAmmo = 34;
                    //add code till we hit to a request
                    using (StreamWriter sw = File.AppendText(m_strFullPath))
                    {
                       sw.WriteLine("m_iDefaultAmmo = " + m_iMagSize + ";"); 
                        
                    } // stop writing here, code asks if the user wants to add the CND system

                    m_iSettingCycler++;
                    break;
                case 3:
                    Console.WriteLine("*SPAWN FUNCTION*");
                    Console.WriteLine("Do you want to enable the Condition system to this weapon? (y/n)");
                    Console.WriteLine("[!] The Condition (CND) system adds durablity to weapons, When CND reaches zero, the player must switch to another weapon or find a new weapon.");
                    m_strCNDOption = Console.ReadLine();

                    if(m_strCNDOption == "y")
                    {
                        using (StreamWriter sw = File.AppendText(m_strFullPath))
                        {
                            sw.WriteLine("m_iSecondaryAmmoType -= 101;"); 

                        } // stop writing here, go back to the original branch


                    }
                    else
                    {

                    }
                    using (StreamWriter sw = File.AppendText(m_strFullPath))
                    {
                        sw.WriteLine("FallInit();");
                        sw.WriteLine("}");
                        sw.WriteLine("");//line skip
                        sw.WriteLine("void C" + m_strInternalWeaponNormal + "::Precache()");
                        sw.WriteLine("{");
                        sw.WriteLine("PRECACHE_MODEL(\"models/v_" + m_strInternalWeaponLower + ".mdl\");");
                        sw.WriteLine("PRECACHE_MODEL(\"models/w_" + m_strInternalWeaponLower + ".mdl\");");
                        sw.WriteLine("PRECACHE_MODEL(\"models/p_" + m_strInternalWeaponLower + ".mdl\");");

                    } // stop writing here, ask user if we have bullet ejections and what type

                    m_iSettingCycler++;

                    break;
                case 4:
                    Console.WriteLine("Does this weapon eject bullets? (b for 9mm, s for shells, n for none)");
                    m_strEjectOption = Console.ReadLine();

                    switch (m_strEjectOption)
                    {
                        case "b":

                            using (StreamWriter sw = File.AppendText(m_strFullPath))
                            {
                                sw.WriteLine("m_iShell = PRECACHE_MODEL(\"models/shell.mdl\");");

                            } // stop writing here, go back to the original branch


                            break;
                        case "s":

                            using (StreamWriter sw = File.AppendText(m_strFullPath))
                            {
                                sw.WriteLine("m_iShell = PRECACHE_MODEL(\"models/shell.mdl\");");

                            } // stop writing here, go back to the original branch

                            break;
                    }

                    m_iSettingCycler++;


                    break;
                case 5:
                    Console.WriteLine("Does this weapon have random damage and random critical hits? y/n");
                    Console.WriteLine("This program will make 16 damage values where one of the do 3 times the damage.");

                    m_strCritOption = Console.ReadLine();

                    switch (m_strCritOption)
                    {
                        case "y":

                            using (StreamWriter sw = File.AppendText(m_strFullPath))
                            {
                                sw.WriteLine("PRECACHE_SOUND(\"weapons/crit.wav\");");

                            } // stop writing here


                            break;
                        default:
                            return;

                    }
                    m_iSettingCycler++;

                    break;

            }

            
                
                
            
        }

    }



    static void FilePathChanger()
    {
        while (true) {
            Console.WriteLine("Put output path:");
            m_strPath = Console.ReadLine();
            m_strFullPath = Path.Combine(m_strPath, m_strName);
            Main(); 
        }
    }



}

