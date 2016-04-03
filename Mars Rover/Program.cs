using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Mars_Rover
{
    class Program
    {
        public static int i_ptX = 0;
        public static int i_ptY = 0;
        public static String s_direction = "";
        private static String validDirections = "NSEW";
        private static String northDirection = "N";
        private static String southDirection = "S";
        private static String eastDirection = "E";
        private static String westDirection = "W";
        private static String validCommands = "LRM";
        private static String leftCommand = "L";
        private static String rightCommand = "R";
        private static String moveCommand = "M";

        private static Boolean isDebugChecked = false;

        private static void debugOut(String msg)
        {
            if (isDebugChecked)
            {
                Console.WriteLine(msg);
            }
        }

        private static String publish_values()
        {
            String s = i_ptX + " " + i_ptY + " " + s_direction;
            Console.WriteLine(s);
            return s;
        }

        private static void doMove()
        {
            switch (s_direction)
            {
                case "N":
                    debugOut("doMove().1 --> (s_direction == northDirection)");
                    i_ptY = i_ptY + 1;
                    break;
                case "E":
                    debugOut("doMove().2 --> (s_direction == eastDirection)");
                    i_ptX = i_ptX + 1;
                    break;
                case "S":
                    debugOut("doMove().3 --> (s_direction == southDirection)");
                    i_ptY = i_ptY - 1;
                    break;
                case "W":
                    debugOut("doMove().4 --> (s_direction == westDirection)");
                    i_ptX = i_ptX - 1;
                    break;
            }
        }

        private static void doSpin(String d)
        {
            s_direction = ((validDirections.IndexOf(d) > -1) || (validCommands.IndexOf(d) > -1)) ? d : s_direction;
            debugOut("doSpin().1 --> d=" + d + ", s_direction=" + s_direction);
        }

        private static void doCommand(String c)
        {
            debugOut("doCommand().1 --> c=" + c);
            switch (c)
            {
                case "L":
                    debugOut("doCommand().2 --> (c == leftCommand)");
                    switch (s_direction)
                    {
                        case "N":
                            debugOut("doCommand().3 --> doSpin(westDirection)");
                            doSpin(westDirection);
                            break;
                        case "W":
                            debugOut("doCommand().4 --> doSpin(southDirection)");
                            doSpin(southDirection);
                            break;
                        case "S":
                            debugOut("doCommand().5 --> doSpin(eastDirection)");
                            doSpin(eastDirection);
                            break;
                        case "E":
                            debugOut("doCommand().6 --> doSpin(northDirection)");
                            doSpin(northDirection);
                            break;
                    }
                    break;
                case "R":
                    debugOut("doCommand().7 --> (c == rightCommand)");
                    switch (s_direction)
                    {
                        case "N":
                            debugOut("doCommand().8 --> doSpin(eastDirection)");
                            doSpin(eastDirection);
                            break;
                        case "E":
                            debugOut("doCommand().9 --> doSpin(southDirection)");
                            doSpin(southDirection);
                            break;
                        case "S":
                            debugOut("doCommand().10 --> doSpin(westDirection)");
                            doSpin(westDirection);
                            break;
                        case "W":
                            debugOut("doCommand().11 --> doSpin(northDirection)");
                            doSpin(northDirection);
                            break;
                    }
                    break;
                case "M":
                    debugOut("doCommand().12 --> (c == moveCommand)");
                    doMove();
                    break;
            }
        }

        private static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static String parseCommand(String c)
        {
            String aTok;
            String aCmd;
            Boolean b;
            Stack items = new Stack();
            String[] toks = c.Split(' ');
            for (int i = 0; i < toks.Length; i++)
            {
                aTok = toks[i];
                debugOut("parseCommand().1 aTok=" + aTok);
                if (aTok.Length > 1)
                {
                    for (var j = 0; j < aTok.Length; j++)
                    {
                        aCmd = aTok.Substring(j, 1);
                        debugOut("parseCommand().2 aCmd=" + aCmd);
                        doCommand(aCmd);
                    }
                }
                else
                {
                    b = IsInteger(aTok);
                    debugOut("parseCommand().3 --> b=" + b);
                    if (b)
                    {
                        items.Push(aTok);
                        debugOut("parseCommand().4 items.Count=" + items.Count);
                        if (items.Count == 2)
                        {
                            i_ptY = Convert.ToInt32(items.Pop());
                            i_ptX = Convert.ToInt32(items.Pop());
                        }
                    }
                    else if (validDirections.IndexOf(aTok) > -1)
                    {
                        s_direction = aTok;
                        debugOut("parseCommand().5 s_direction=" + s_direction);
                    }
                    else if (validCommands.IndexOf(aTok) > -1)
                    {
                        debugOut("parseCommand().6 doCommand(" + aTok + ")");
                        doCommand(aTok);
                    }
                }
            }
            return publish_values();
        }

        static void Main(string[] args)
        {
            String cmd = "1 2 N";
            String expected = "1 2 N";
            String x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #1");
            }

            cmd = "L";
            expected = "1 2 W";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #2");
            }

            cmd = "M";
            expected = "0 2 W";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #3");
            }

            cmd = "L";
            expected = "0 2 S";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #4");
            }

            cmd = "M";
            expected = "0 1 S";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #5");
            }

            cmd = "L";
            expected = "0 1 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #6");
            }

            cmd = "M";
            expected = "1 1 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #7");
            }

            cmd = "L";
            expected = "1 1 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #8");
            }

            cmd = "M";
            expected = "1 2 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #9");
            }

            cmd = "M";
            expected = "1 3 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #10");
            }

            cmd = "3 3 E";
            expected = "3 3 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #11");
            }

            cmd = "M";
            expected = "4 3 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #12");
            }

            cmd = "M";
            expected = "5 3 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #13");
            }

            cmd = "R";
            expected = "5 3 S";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #14");
            }

            cmd = "M";
            expected = "5 2 S";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #15");
            }

            cmd = "M";
            expected = "5 1 S";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #16");
            }

            cmd = "R";
            expected = "5 1 W";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #17");
            }

            cmd = "M";
            expected = "4 1 W";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #18");
            }

            cmd = "R";
            expected = "4 1 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #19");
            }

            cmd = "R";
            expected = "4 1 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #20");
            }

            cmd = "M";
            expected = "5 1 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #21");
            }

            cmd = "1 2 N";
            expected = "1 2 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #22");
            }

            cmd = "LMLMLMLMM";
            expected = "1 3 N";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #23");
            }

            cmd = "3 3 E";
            expected = "3 3 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #24");
            }

            cmd = "MMRMMRMRRM";
            expected = "5 1 E";
            x = parseCommand(cmd);
            if (x != expected)
            {
                Console.WriteLine("ERROR #25");
            }
        }
    }
}
