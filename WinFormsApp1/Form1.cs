using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        private static int secondsElapsed = 0;
        private static bool timerRunning = false;
        private static Thread timerThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void process_Click(object sender, EventArgs e)
        {
            if (strInput.Text.Length > 0)
            {

                //Timer 
                StartTimer(1000);

                //Clear interface
                strOutput.Clear() ;
                arrangedInput.Clear();
                time.Text = "";
                varnb.Text = "";
                solnb.Text = "";

                //Ensuring the number of clauses is even or repeat the last clause and extract variables V
                string[] splitOut = strInput.Text.Split('∧');
                string[] clauses = new string[splitOut.Length % 2 == 0 ? splitOut.Length : splitOut.Length + 1];
               
                for (int i = 0; i < splitOut.Length; i++)
                {
                    clauses[i] = splitOut[i].Replace("(", "").Replace(")", "").Trim();
                }
                if (clauses.Length > splitOut.Length)
                    clauses[clauses.Length - 1] = clauses[clauses.Length - 2].Trim();

                List<string> V = new List<string>();
                foreach (string clause in clauses)
                {
                    string[] vars = clause.Split('∨');
                    foreach(string vv in vars)
                    {
                        V.Add(vv.Trim());
                    }
                }

                //Arrange variables
                arrange(V);
                //Show arranged variables
                arrangedInput.Text = arrangeInterfaceInput(V);
                varnb.Text = V.Count.ToString();
                

                //Build inputList
                int[] RI = new int[V.Count];// RI: R1I1R2I2R3I3, R1I1R2I2R3I3,..
                for (int tc = 0; tc < V.Count; tc += 6)//Go to the next 6 variables(two-clauses)
                {
                    for(int i = tc; i < tc+3; i++)
                    {
                        int ri=2*(i-tc);
                        if (V[i]==V[i + 3])       //double negation lies here. 
                        { RI[tc + ri] = 1; RI[tc + ri + 1] = 0; }                  // R1=1; I1=0;
                        else if (V[i ]==("¬" + V[i + 3]) || V[i + 3]==("¬" + V[i]))
                        { RI[tc + ri] = 1; RI[tc + ri + 1] = 1; } //R1=1;I1=1
                        else
                        { RI[tc + ri] = 0; RI[tc + ri + 1] = 0; } //R1=0,I1=0
                    }
                }

                displayInputlist(RI);
                //Prepare the pre-fixed table
                int[][] A = fillA();

                // Get listGroups, list of outputs of each two-clauses
                List<List<int[]>> listGroups = new List<List<int[]>>();
                for (int vi = 0; vi < V.Count; vi += 6)//Go to the start of the next group; RI.Count=V.Count 
                {
                    tPass(A, RI.ToList().GetRange(vi, 6));    //tPass the table by its inputs RI

                    List<int[]> g1 = new List<int[]>();
                    for (int i = 0; i <= 48; i++)
                    {
                        if (A[i][6] == 1)   // Copy only valid outputs
                        {
                            int[] tempG = new int[6];
                            for (int j = 0; j <= 5; j++)
                            { tempG[j] = A[i][j]; }
                            g1.Add(tempG);
                        }
                    }
                    listGroups.Add(g1); //Add the group output to the listGroups 
                    for (int i = 0; i <= 48; i++) A[i][6] = 1; //reset table
                }
                
                //Intersect
                int stage = 1;
                List<int[]> currGroup = listGroups[0];
                List<int[]> solutionsGroup = new List<int[]>();
                while (stage < listGroups.Count)
                {
                    List<int[]> nextStageGroupe = listGroups[stage];
                    List<int> sim = new List<int>(); List<int> dif = new List<int>(); //12:max number of possible similaritie or dif (two-groups 12 vars)
                
                    //Find similarities between the two groups at some stage of intersection
                    for (int j = stage * 6; j < stage * 6 + 6; j++) //Loop on the next group columns
                    {
                        for (int i = 0; i < stage * 6; i++)
                        {
                            if (V[i].Trim() == V[j].Trim())
                            {
                                sim.Add(i);                    //currGroup index of similar
                                sim.Add(j - (stage * 6));      // stageGroup index of similar
                                break;               //Take the first occurrence of similarity
                            }
                            else if (V[j].Trim()=="¬" + V[i].Trim() || V[i].Trim() == "¬" + V[j].Trim())
                            {
                                dif.Add(i);            //currGroup index of difference
                                dif.Add(j - (stage * 6));    //stageGroup index of difference  dif.add(j-stage*6);    //stageGroup index of difference
                                break;                 //take the first occurrence of difference
                            }
                        }
                    }

                    //Compare and build the next solutionsGroup
                    solutionsGroup = new List<int[]>();
                    for (int i = 0; i < currGroup.Count; i++)
                    {
                        for (int j = 0; j < nextStageGroupe.Count; j++)
                        {
                            bool conflict = false;
                            for (int k = 0; k < sim.Count - 1; k += 2)
                            {
                                if (currGroup[i][sim[k]] != nextStageGroupe[j][sim[k + 1]])//should be similar
                                {
                                    conflict = true;
                                    break;
                                }
                            }
                            if(!conflict)
                            for (int k = 0; k < dif.Count - 1; k += 2)
                            {
                                if (currGroup[i][dif[k]] == nextStageGroupe[j][dif[k + 1]])//should be different
                                {
                                    conflict = true;
                                    break;
                                }
                            }
                            if (conflict)
                            {
                                continue; // nextStageGroup row conflicted with currGroup row
                            }
                            solutionsGroup.Add(currGroup[i].Concat(nextStageGroupe[j]).ToArray());
                        }
                    }
                    
                    if (solutionsGroup.Count == 0)
                    {
                        //SAT is unsatisfiable 
                        strOutput.Text = "3-SAT is Unsatisfiable";
                        solnb.Text = solutionsGroup.Count.ToString();
                        StopTimer();
                        time.Text = "Elapsed time: " + secondsElapsed + " seconds";
                        break;
                    }
                    sim = null;
                    dif = null;
                    GC.Collect();
                    currGroup = solutionsGroup;
                    stage++;

                }
                if (timerRunning)
                {
                    //Output results
                    StopTimer();
                    
                    //Extruct distinct variables
                    var distinctVars = V.Distinct();
                    var positifDistinctVar =new  List<string>();
                    foreach (var var in distinctVars)
                    {
                        string temp = "";
                        if (var.StartsWith("¬"))
                        {
                            temp = var.Substring(1);
                        }
                        else
                            temp = var;
                        positifDistinctVar.Add(temp);
                    }
                    distinctVars = positifDistinctVar.Distinct();

                    distinctVars= distinctVars.Distinct();
                    List<List<int>> sols=new List<List<int>>();

                    foreach (int[] solution in solutionsGroup)
                    {
                        List<int> so = new List<int>();
                        foreach (var var in distinctVars)
                        {
                            for (int i = 0; i < solution.Length; i++)
                            {
                                if (V[i] == var)
                                {
                                    so.Add(solution[i]);
                                    break;
                                }
                                else if (V[i] == "¬" + var)
                                {
                                    so.Add(1 - solution[i]);
                                    break;
                                }
                            }
                        }
                        sols.Add(so);
                    }
                    
                    for(int i = 0; i < distinctVars.Count(); i++)
                    {
                        strOutput.Text += distinctVars.ElementAt(i);
                        if (i < distinctVars.Count() - 1)
                        {
                            strOutput.Text += ", ";
                        }
                    }
                    strOutput.Text += " \n";
                    foreach (var sol in sols)
                    {
                        strOutput.Text += "[";
                        for (int i = 0; i < sol.Count; i++)
                        {
                            strOutput.Text += sol[i];
                            if (i < sol.Count - 1)
                            {
                                strOutput.Text += ", ";
                            }
                        }
                        strOutput.Text += "] \n";
                    }

                    //foreach (int[] solution in solutionsGroup)
                    //{
                    //    strOutput.Text += "[";
                    //    for (int i = 0; i < solution.Length; i++)
                    //    {
                    //        strOutput.Text += solution[i];
                    //        if (i < solution.Length - 1)
                    //        {
                    //            strOutput.Text += ", ";
                    //        }
                    //    }
                    //    strOutput.Text += "]";
                    //}
                    solnb.Text = solutionsGroup.Count.ToString();
                    time.Text = "Elapsed time: " + secondsElapsed + " seconds";

                }

            }
        }

        private void displayInputlist(int[] RI)
        {
            inputArray.Text = "[";
            for (int i = 0; i < RI.Length; i++)
            {
                inputArray.Text += RI[i];
                if ((i + 1) % 6 == 0)
                {
                    inputArray.Text += "]";
                    if (i < RI.Length - 1)
                        inputArray.Text += " [";
                }
                else
                {
                    if (i % 2 == 0)
                        inputArray.Text += "  :  ";
                    else inputArray.Text += ",   ";
                }
            }
        }

        private string arrangeInterfaceInput(List<string> v)
        {
            string outStr = "";
            for (int i=0;i<v.Count;i+=3)
            {
                outStr += "(" + v[i] + "∨" + v[i + 1] + "∨" + v[i + 2] + ")";
                if (i+2 < v.Count - 1)
                    outStr += "∧";
            }
            return outStr;
        }

        private int[][] fillA()
        {
            int[][] A= new int[49][];
            for (int i = 0; i < A.Length; i++)
                A[i] = new int[6];
            A[0] = new int[] { 0, 0, 1, 0, 0, 1, 1 };
            A[1] = new int[] { 0, 0, 1, 0, 1, 0, 1 };
            A[2] = new int[] { 0, 0, 1, 0, 1, 1, 1 };
            A[3] = new int[] { 0, 0, 1, 1, 0, 0, 1 };
            A[4] = new int[] { 0, 0, 1, 1, 0, 1, 1 };
            A[5] = new int[] { 0, 0, 1, 1, 1, 0, 1 };
            A[6] = new int[] { 0, 0, 1, 1, 1, 1, 1 };

            A[7] = new int[] { 0, 1, 0, 0, 0, 1, 1 };
            A[8] = new int[] { 0, 1, 0, 0, 1, 0, 1 };
            A[9] = new int[] { 0, 1, 0, 0, 1, 1, 1 };
            A[10] = new int[] { 0, 1, 0, 1, 0, 0, 1 };
            A[11] = new int[] { 0, 1, 0, 1, 0, 1, 1 };
            A[12] = new int[] { 0, 1, 0, 1, 1, 0, 1 };
            A[13] = new int[] { 0, 1, 0, 1, 1, 1, 1 };

            A[14] = new int[] { 0, 1, 1, 0, 0, 1, 1 };
            A[15] = new int[] { 0, 1, 1, 0, 1, 0, 1 };
            A[16] = new int[] { 0, 1, 1, 0, 1, 1, 1 };
            A[17] = new int[] { 0, 1, 1, 1, 0, 0, 1 };
            A[18] = new int[] { 0, 1, 1, 1, 0, 1, 1 };
            A[19] = new int[] { 0, 1, 1, 1, 1, 0, 1 };
            A[20] = new int[] { 0, 1, 1, 1, 1, 1, 1 };

            A[21] = new int[] { 1, 0, 0, 0, 0, 1, 1 };
            A[22] = new int[] { 1, 0, 0, 0, 1, 0, 1 };
            A[23] = new int[] { 1, 0, 0, 0, 1, 1, 1 };
            A[24] = new int[] { 1, 0, 0, 1, 0, 0, 1 };
            A[25] = new int[] { 1, 0, 0, 1, 0, 1, 1 };
            A[26] = new int[] { 1, 0, 0, 1, 1, 0, 1 };
            A[27] = new int[] { 1, 0, 0, 1, 1, 1, 1 };

            A[28] = new int[] { 1, 0, 1, 0, 0, 1, 1 };
            A[29] = new int[] { 1, 0, 1, 0, 1, 0, 1 };
            A[30] = new int[] { 1, 0, 1, 0, 1, 1, 1 };
            A[31] = new int[] { 1, 0, 1, 1, 0, 0, 1 };
            A[32] = new int[] { 1, 0, 1, 1, 0, 1, 1 };
            A[33] = new int[] { 1, 0, 1, 1, 1, 0, 1 };
            A[34] = new int[] { 1, 0, 1, 1, 1, 1, 1 };


            A[35] = new int[] { 1, 1, 0, 0, 0, 1, 1 };
            A[36] = new int[] { 1, 1, 0, 0, 1, 0, 1 };
            A[37] = new int[] { 1, 1, 0, 0, 1, 1, 1 };
            A[38] = new int[] { 1, 1, 0, 1, 0, 0, 1 };
            A[39] = new int[] { 1, 1, 0, 1, 0, 1, 1 };
            A[40] = new int[] { 1, 1, 0, 1, 1, 0, 1 };
            A[41] = new int[] { 1, 1, 0, 1, 1, 1, 1 };

            A[42] = new int[] { 1, 1, 1, 0, 0, 1, 1 };
            A[43] = new int[] { 1, 1, 1, 0, 1, 0, 1 };
            A[44] = new int[] { 1, 1, 1, 0, 1, 1, 1 };
            A[45] = new int[] { 1, 1, 1, 1, 0, 0, 1 };
            A[46] = new int[] { 1, 1, 1, 1, 0, 1, 1 };
            A[47] = new int[] { 1, 1, 1, 1, 1, 0, 1 };
            A[48] = new int[] { 1, 1, 1, 1, 1, 1, 1 };

            return A;
        }

        public int[][] tPass(int[][] A, List<int> RI)
        {
            //Set the flags of two-clauses outputs based on 6 inputs from the input array.
            for (int i = 0; i < 48; i++)
                for (int j = 0; j < 3; j++)
                {
                    int ri=2*j;
                    if (RI[ri] == 1 && RI[ri+1] == 1 && A[i][j] == A[i][3 + j]) // connected & inversed => must be different. 
                    { A[i][6] = 0; continue; }
                    if (RI[ri] == 1 & RI[ri+1] == 0 && A[i][j] != A[i][3 + j])   // connected & not inversed => must be equal.
                    { A[i][6] = 0; continue; }
                }
            return A;
        }
        public void arrange(List<string> V)
        {
            for (int tc = 0; tc < V.Count; tc += 6)    //jump to the next 6 literals representing two-clauses
            { 
                for(int i = tc; i < tc+3; i++)
                for (int j = tc + 3; j < tc + 6; j++)
                    if ((V[i]==V[j] || V[i]==("¬" + V[j])
                                       || V[j]==("¬" + V[i]))
                                       && (j != i + 3))
                    {
                        //SWAP
                        string temp = V[j];
                        V[j] = V[i + 3];
                        V[i + 3] = temp;
                    }
            }


        }
        
        public static void StartTimer(int interval)
        {
            if (timerRunning)
            {
                return; // Prevent starting multiple timers
            }

            timerRunning = true;
            secondsElapsed = 0; // Reset elapsed time on start

            timerThread = new Thread(() => TimerLoop(interval));
            timerThread.Start();
        }

        public static void StopTimer()
        {
            if (!timerRunning)
            {
                return; // Prevent stopping a non-running timer
            }

            timerRunning = false;
            timerThread.Join(); // Wait for the timer thread to finish
        }

        private static void TimerLoop(int interval)
        {
            while (timerRunning)
            {
                Thread.Sleep(interval);
                secondsElapsed++;

                // Add your code to be executed every second here
            }
        }
        
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}