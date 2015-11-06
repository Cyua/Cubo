using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;
	public Texture2D[,] hintTex = new Texture2D[10, 3];

//	private ArrayList rawInput = new ArrayList();
	private int[,,] shape;					//original shape of cubes
	private int[,] hintHW, hintHL, hintWL;	//hints
	private GameObject cubes;				//cube model
	private int l=0,w=0,h=0;				//length, width, height of the shape
	private int mx=0,my=0,mz=0;				//center vector3 of the shape
	private float px=0,py=0,pz=0;			//center vector3 of the Cubes

	/*************************** GUI begins *****************************/
	private Rect penal;
	private int stepCount=0;
	private const int MAXSTEP = 12;
	private string[] tutorialStep = new string[MAXSTEP];

	void OnGUI(){
		
		//		GUIStyle background = new GUIStyle ();		//set the background image to fill the screen
		//		background.normal.background = image;
		//		GUI.Label (new Rect(0,0,Screen.width,Screen.height),"",background);
		penal = new Rect (0,0,Screen.width,Screen.height/4);
		penal = GUI.Window (0,penal,giveTutorial,"Welcome to Tutorial");
		if (GUI.Button (new Rect (Screen.width - 70, Screen.height - 40, 60, 30), "Menu")) {
			Application.LoadLevel("mainMenu");
		}
		
	}
	void giveTutorial(int windowId){
		if (windowId == 0) {
			GUIStyle textFont = new GUIStyle ();
			textFont.fontSize = 20;									//font size
			textFont.normal.textColor = new Color (255, 255, 255);		//font color
			textFont.wordWrap = true;
			GUI.Label (new Rect (30, 30, penal.width - 60, penal.height - 60), tutorialStep [stepCount],textFont);		//set text area

			if (GUI.Button (new Rect (penal.width - 80, penal.height - 40, 65, 30), "Next")) {
				stepCount++;
				if (stepCount >= MAXSTEP)
					stepCount = MAXSTEP - 1;
			}
		}
	}
	/***************************** GUI ends *********************************/

	int returnMiddle(int x){
		double tempf = x * 1.0 / 2;
		int tempi = x / 2;
		if (tempf > tempi)
			return tempi+1;
		else
			return tempi;
	}

	Texture2D LoadTex(int number, int type) {
		Texture2D tex = null;
		string filename;
		string append;
		if (number == 0 && type == 1)
			tex = (Texture2D)Resources.Load ("Textures/null");
		else {
			if (type == 0)
				append = "";
			else if (type == 1)
				append = "_c";
			else
				append = "_s";
			filename = "Textures/t" + number + append;
			tex = (Texture2D)Resources.Load (filename);
		}
		return tex;
	}

	Texture2D TransformHint(int number) {
		if (number == -1)
			return hintTex [0, 1];
		else if (number < 10)
			return hintTex [number, 0];
		else if (number < 100)
			return hintTex [number / 10, 1];
		else
			return hintTex [number / 100, 2];
	}

	//read "level.txt"
	void readInput(string target){
		TextAsset level = (TextAsset)Resources.Load (target);
		string[] lines = level.text.Split('\n');			//fill the rawInput

		// First line: Height, width, length
		string[] hwl = lines[0].Split(',');
		h = int.Parse(hwl[0]);
		w = int.Parse(hwl[1]);
		l = int.Parse(hwl[2]);

		shape = new int[h, w, l];
		hintHW = new int[h, w];
		hintHL = new int[h, l];
		hintWL = new int[w, l];

		// Read shape
		for (int i = 0; i < h; i++) {
			string line = lines[i+1];
			string[] second = line.Split(',');
			for (int j = 0; j < w; j++) {
				string[] third = second[j].Split(' ');
				for (int k = 0; k < l; k++) {
					shape[i, j, k] = 1;
				}
			}
		}

		// Read hint
		// height-width
		string[] hw = lines[h+1].Split(',');
		for (int i = 0; i < h; i++) {
			string[] second = hw [i].Split (' ');
			for (int j = 0; j < w; j++)
				hintHW [i, j] = int.Parse (second [j]);
		}
		// height-length
		string[] hl = lines[h+2].Split(',');
		for (int i = 0; i < h; i++) {
			string[] second = hl [i].Split (' ');
			for (int j = 0; j < l; j++)
				hintHL [i, j] = int.Parse (second [j]);
		}

		// width-length
		string[] wl = lines[h+3].Split(',');
		for (int i = 0; i < w; i++) {
			string[] second = wl [i].Split (' ');
			for (int j = 0; j < l; j++)
				hintWL [i, j] = int.Parse (second [j]);
		}
	}
	

	void Start () {
		/****************************** tutorial **********************************/
		tutorialStep [0] = "Cubo是一个三维益智游戏，游戏的目的是通过消除一个长方体上的小立方块，最后" +
			"构造出一个特定的有意义的三维物体（如由小方块组成的字母、手机、房子等），在游戏中，由立方体拼成的大长" +
			"方体上，每一行或每一列至多有一个数字与之对应。";
		tutorialStep [1] = "该数字表明在最终成品中，该行或该列剩余的立方体数目。玩家根据数字进行逻辑推理，敲除不需要的方块，" +
			"标记确定会剩余的方块，敲除至剩余形状与目标形状一致时，即可通关。当然，为了增大难度，有些方块上并没有标记任何数字，不" +
			"过我们可以保证运用逻辑推理，一定是可以推导出最终形态的哦～";
		tutorialStep [2] = "例如，我们看下面这个长方体的其中一面。我们可以看到在这个面上有许多标着数字0的，" +
			"说明最终成品中这一条上没有方块存在，因此我们可以把它们全都敲除。";
		tutorialStep [3] = "在游戏进行时，玩家可以按住空格后用鼠标点击对应的小方块，即可敲除方块了。" +
			"按照上一步的分析，敲除后的物体看起来像是这样。";
		tutorialStep [4] = "让我们旋转起来看看吧，机智的你是否猜出了最终形态呢？";
		tutorialStep [5] = "让我们把目光投向这个物体的最顶层，我们可以看到现在只剩两条方块了。" +
			"在每一条方块的两侧写着1，说明这一条中只能留下一个方块。在两条方块的中央小方块上，写着一个2" +
			"，并且这个2被圆圈包围着。圆圈标记说明这个方向上存在方块，且中间有断隔。";
		tutorialStep [6] = "因此我们可以很容易的推断出，应该留下中间两个方块。我们来看看敲除后的效果吧！";
		tutorialStep [7] = "要注意的是，如果你在敲除过程中出错了，敲了错误的方块，不要担心，我们会将这个方块标" +
				"记成红色并设置成不可敲除,就像下面这样。但是可不要以为这样一来你就可以胡乱敲除了，" +
				"如果红色方块出现三个，游戏就失败了喔！";
		tutorialStep [8] = "如果你现在担心手抖敲错方块的话，不要慌！我们可以按住X键，然后点击相应的方块，" +
			"这样就可以把方块锁住，锁住的方块将呈现蓝色并无法被敲除哦～当然，解锁也是同样的方法。";
		tutorialStep [9] =  "接下来，继续进行逻辑推断，我们能很快的推导出接下来应该把哪些方块进行敲除。" +
			"让我们来看看最终结果吧！";
		tutorialStep [10] = "检查一下各个方块上的数字，看看我们是不是得到了正确结果？" +
			"当然，如果在游戏中我们消除到正确结果以后，系统会自动弹窗提示游戏胜利喔！" +
			"看出来是什么了吗？让我们给它上个色试试吧！";
		tutorialStep [11] = "本次教程就到此为止啦！谢谢观看～";
		/****************************** tutorial **********************************/

		// load textures;
		for (int i = 0; i <= 9; i++)
			for (int j = 0; j <= 2; j++)
				hintTex[i, j] = LoadTex(i, j);

		readInput ("tutorial");
		mx = returnMiddle (l)-1;  //calculate the middle point
		my = returnMiddle (h);
		mz = returnMiddle (w)-1;

		px = transform.position.x;  //parent's position
		py = transform.position.y;
		pz = transform.position.z;

		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				for (int k = 0; k < l; k++) {
					if (shape[i,j,k] == 1) {
						cubes = Instantiate (instantiate, transform.position, transform.rotation) as GameObject;
						cubes.transform.SetParent(this.transform,true);
						cubes.transform.position = new Vector3(k-mx+px, h-i-my+py, j-mz+pz);
						cubes.GetComponent<SingleCube>().SetHint(TransformHint(hintHL[i, k]), 
						                                         TransformHint(hintHW[i, j]),
						                                         TransformHint(hintWL[j, k]));
					}
				}
			}
		}

		Debug.Log (transform.position);
		Debug.Log (transform.rotation);
	}

	void knock(){
		GameObject [] childCube = GameObject.FindGameObjectsWithTag("cubeTag");
		foreach(GameObject itemCube in childCube){
			itemCube.SendMessage("tutorialKnock",stepCount);
		}
	}

	void Update () {
		// Rotate the cube as mouse drags
		if (stepCount < 2 || stepCount >=4)
			transform.RotateAround (transform.position, Vector3.down, Time.deltaTime * rotate_speed);
		if (stepCount == 2) {
			transform.rotation = new Quaternion (0, 0, 0, 1);
		} else if (stepCount == 3 || stepCount>=6) {
			knock ();
		} 

	}
}
