using UnityEngine;
using System.Collections;

public class WoestijnScript : MonoBehaviour {
	public Terrain terrain;
	public int levelgrootte;
	public int aantalHeuvels;
	public int aantalVijvers;
	public int aantalBergen;
	public int aaantalSloten;
	public int aantalMeren;
	public int aantalBomen;
	void generateTerrain(){
		TerrainData terraindata = terrain.terrainData;
		int heightmapx = terraindata.heightmapWidth;
		int heightmapz = terraindata.heightmapHeight;
		float [] positionsx = new float[aantalHeuvels];
		float [] positionsz = new  float[aantalHeuvels];
		double [] waterx = new double[aantalVijvers]; 
		double [] waterz = new double[aantalVijvers]; 
		double [] waterr = new double[aantalVijvers]; 
		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		GameObject Schaap = Resources.Load ("sheep") as GameObject;
		GameObject Steen = Resources.Load ("RockMesh") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer> ().bounds.size.x;
		int levelsizex = (int)(levelgrootte * lengteHek);
		int levelsizez = (int)(levelgrootte * lengteHek);
		float beginhoogte = 0.008f;
		float[,] heights = new float[heightmapx, heightmapz];
		float terrainsizex = terraindata.size.x;
		float terrainsizez = terraindata.size.z;
		float verhoudingx = levelsizex / terrainsizex;
		float verhoudingz = levelsizez / terrainsizez;
		float beginx = 0.5f * (float)terrainsizex - 0.5f * (float)levelsizex;
		float eindx = 0.5f * (float)terrainsizex + 0.5f * (float)levelsizex;
		float beginz = 0.5f * (float)terrainsizez - 0.5f * (float)levelsizez;
		float eindz = 0.5f * (float)terrainsizez + 0.5f * (float)levelsizez;
		float hmlevellengthx = (int)(heightmapx * verhoudingx);
		float hmlevellengthz = (int)(heightmapz * verhoudingz);
		int hmlevelbeginx = (int)(0.5 * heightmapx - 0.5 * hmlevellengthx);
		int hmleveleindx = (int)(0.5 * heightmapx + 0.5 * hmlevellengthx);
		int hmlevelbeginz = (int)(0.5 * heightmapx - 0.5 * hmlevellengthz);
		int hmleveleindz = (int)(0.5 * heightmapx + 0.5 * hmlevellengthz);
		
		for (int x = 0; x < heightmapx; x++) {
			for (int z = 0; z < heightmapz; z++) {
				heights [x, z] = beginhoogte;
			}
		}
		
		terraindata.SetHeights (0, 0, heights);
		
		
		// heuvels genereren in het level
		for (int i = 0; i < aantalHeuvels; i++) {
			int minradius = (int)(terrainsizex / 55);
			int maxradius = (int)(terrainsizex / 40);
			int heuvelradius = (int)(Random.Range (minradius, maxradius));
			int xbegin = (int)(Random.Range (hmlevelbeginx + maxradius, hmleveleindx - maxradius));
			int zbegin = (int)(Random.Range (hmlevelbeginz + maxradius, hmleveleindz - maxradius));
			positionsx [i] = xbegin;
			positionsz [i] = zbegin;
			float top = 0.04f;
			float helling = (float)(top * (0.01f / 0.05f));
			for (int r = 1; r < heuvelradius; r++) {
				for (int d = 0; d < 360; d++) {
					int x = xbegin + (int)(r * Mathf.Cos (d * Mathf.PI / 180));
					int z = zbegin + (int)(r * Mathf.Sin (d * Mathf.PI / 180));
					float hoogte = Mathf.Max ((float)(top - (Mathf.Pow ((r * helling), 2))), (float)(beginhoogte * 1.1));
					if (heights [Mathf.Max (x, 0), Mathf.Max (z, 0)] < hoogte) {
						heights [Mathf.Max (x, 0), Mathf.Max (z, 0)] = hoogte;
					}
				}
			}
		}

		// vijvers genereren level
		for (int i = 0; i < aantalVijvers; i++) {
			float minradius = terrainsizex / 90;
			float maxradius = terrainsizex / 80;
			double vijverradius = (double)Random.Range (minradius, maxradius);
			double xbegin = Random.Range (hmlevelbeginx + maxradius, hmleveleindx - maxradius);
			double zbegin = Random.Range (hmlevelbeginz + maxradius, hmleveleindz - maxradius);
			waterx [i] = xbegin;
			waterz [i] = zbegin;
			waterr [i] = vijverradius;
			for (int r = 0; r < vijverradius; r++) {
				for (int d = 0; d < 360; d++) {
					int x = (int)xbegin + (int)(r * Mathf.Cos (d * Mathf.PI / 180));
					int z = (int)zbegin + (int)(r * Mathf.Sin (d * Mathf.PI / 180));
					heights [x, z] = 0;
				}
			}
		}

		for (int x = 0; x < heightmapx; x++) {
			for (int z = 0; z < heightmapz; z++) {
				if (heights [x, z] >= beginhoogte) {
					heights [x, z] += Mathf.Abs (Mathf.Sin ((Random.value * x + Random.value * z) * 10) / 50);
				}
			}
		}
		//Hekjes spawnen
		int aantalhekjesx = (int)(levelsizex / lengteHek);
		int aantalhekjesz = (int)(levelsizez / lengteHek);
		float grondhoogte = 12.5f;
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek = Instantiate (hekPrefab);
			hek.transform.position = new Vector3 (beginx + i * lengteHek, grondhoogte, beginz);
		}
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek2 = Instantiate (hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, grondhoogte, eindz);
			if (i == (int)(aantalhekjesx / 2)) {
				hek2.tag = "Hek";
			}
		}
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek2 = Instantiate (hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, grondhoogte, eindz + 2 * lengteHek);
			GameObject schaap = Instantiate (Schaap);
			schaap.transform.position = new Vector3 (beginx + i * lengteHek, grondhoogte, eindz + lengteHek);
			schaap.transform.localScale = new Vector3 (3, 3, 3);
			schaap.transform.eulerAngles = new Vector3 (0, Random.value * 360, 0);
		}
		for (int i = 0; i < aantalhekjesz+3; i++) {
			GameObject hek = Instantiate (hekPrefab);
			hek.transform.position = new Vector3 (beginx, grondhoogte, beginz + i * lengteHek);
			hek.transform.eulerAngles = new Vector3 (0, 90, 0);
		}
		for (int i = 0; i < aantalhekjesz+3; i++) {
			GameObject hek2 = Instantiate (hekPrefab);
			hek2.transform.position = new Vector3 (eindx, grondhoogte, beginz + i * lengteHek);
			hek2.transform.eulerAngles = new Vector3 (0, 90, 0);
		}
		//stenen spawnen
		for (int i = 0; i<3; i++) {
			GameObject steen = Instantiate (Steen);
			steen.transform.position = new Vector3 (Random.Range (0, terrainsizex), 1000, Random.Range (0, terrainsizez));
		}
		terraindata.SetHeights (0, 0, heights);
		for (int i = 0; i < 4; i++) {
			Smooth ();
		}
		//bomen spawnen
		GameObject Palmboom1 = Resources.Load ("Palm1") as GameObject;
		GameObject Palmboom2 = Resources.Load ("Palm2") as GameObject;
		GameObject Palmboom3 = Resources.Load ("Palm4") as GameObject;
		GameObject Palmboom4 = Resources.Load ("Palm8") as GameObject;
		GameObject Palmboom5 = Resources.Load ("Palm9") as GameObject;
		GameObject Palmboom6 = Resources.Load ("Palm10") as GameObject;

		GameObject Dodeboom = Resources.Load ("DeadTree") as GameObject;
		GameObject Cactus1 = Resources.Load ("Cactus1") as GameObject;
		GameObject Cactus2 = Resources.Load ("Cactus2") as GameObject;

		for (int i = 0; i<aantalBomen; i++) {
			double x = Random.Range (hmlevelbeginx + 2, hmleveleindx - 2);
			double z = Random.Range (hmlevelbeginz + 2, hmleveleindz - 2);
			float xx = (float)(z * terrainsizez / heightmapz);
			float zz = (float)(x * terrainsizex / heightmapx);
			if (watercheck (waterx, waterz, waterr, x, z) == 1) {
				float random = Random.value;
				if (random < (0.16f)) {
					GameObject palm = Instantiate (Palmboom1);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));

						}
					}
				} else if (random > (0.16f) && random < (0.33f)) {
					GameObject palm = Instantiate (Palmboom2);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));
							
						}					
					}
				} else if (random > (0.33f) && random < (0.5f)) {
					GameObject palm = Instantiate (Palmboom3);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));
							
						}					
					}
				} else if (random > (0.5f) && random < (0.66f)) {
					GameObject palm = Instantiate (Palmboom4);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));
							
						}					
					}
				} else if (random > (0.66f) && random < (0.83f)) {
					GameObject palm = Instantiate (Palmboom5);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));
							
						}					
					}
				} else {
					GameObject palm = Instantiate (Palmboom6);
					palm.transform.position = new Vector3 (xx, 1000, zz);
					palm.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					palm.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
					RaycastHit test;
					Ray testray = new Ray (palm.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						if(test.distance<10){
							palm.transform.Translate (new Vector3 (0, -2000, 0));
						}
						else{
							palm.transform.Translate (new Vector3 (0, -test.distance, 0));
							
						}				
					}
				}
			}
			if (watercheck (waterx, waterz, waterr, x, z) == 2 && Random.value < 0.001f) {
				if (Random.value > 0.5) {
					GameObject boom = Instantiate (Dodeboom);
					boom.transform.position = new Vector3 (xx, 1000, zz);
					boom.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
					boom.transform.localScale = new Vector3 (2, 2, 2);
					RaycastHit test;
					Ray testray = new Ray (boom.transform.position, Vector3.down);
					if (Physics.Raycast (testray, out test)) {
						boom.transform.Translate (new Vector3 (0, -test.distance, 0));
					}
				} else {
					if (Random.value > 0.5) {
						GameObject cactus = Instantiate (Cactus1);
						cactus.transform.position = new Vector3 (xx, 1000, zz);
						cactus.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
						RaycastHit test;
						Ray testray = new Ray (cactus.transform.position, Vector3.down);
						if (Physics.Raycast (testray, out test)) {
							cactus.transform.Translate (new Vector3 (0, -test.distance, 0));
						}
					} else {
						GameObject cactus = Instantiate (Cactus2);
						cactus.transform.position = new Vector3 (xx, 1000, zz);
						cactus.transform.eulerAngles = new Vector3 (0, Random.Range (0, 360), 0);
						RaycastHit test;
						Ray testray = new Ray (cactus.transform.position, Vector3.down);
						if (Physics.Raycast (testray, out test)) {
							cactus.transform.Translate (new Vector3 (0, -test.distance, 0));
						}
					}
				}
			}
		}
	}
	public bool positioncheck(float x, float z, float[] xh, float[] zh){
		for(int i = 0; i < aantalHeuvels; i++){
			float disx = x - xh[i]-10;
			float disz = z - zh[i]+10;
			float distance = Mathf.Sqrt(disx*disx + disz*disz);
			if(distance<15){
				return false;
			}
		}
		return true;
	}
	public int watercheck(double [] waterx, double [] waterz, double[] waterstraal, double x, double z){
		double minafstand = 1000;
		for (int i = 0; i < waterx.Length; i++) {
			double xw = waterx[i];
			double zw = waterz[i];
			double r  = waterstraal[i];
			double disx = x - xw;
			double disz = z - zw;
			double distance = Vector2.Distance(new Vector2((float)x,(float)z),new Vector2((float)xw,(float)zw));
			double afstand = distance -r;
			minafstand = Mathf.Min((float)minafstand,(float)afstand);	
		}
		if(minafstand>7&&minafstand<10){
			return 1;
		}
		if(minafstand>=10){
			return 2;
		}
		return 0;
	}
	private void Smooth()
	{
		float[,] height = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
		                                                 terrain.terrainData.heightmapHeight);
		float k = 0.5f;
		for (int x = 1; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x - 1, z] * (1 - k) +	height[x, z] * k;
		
		for (int x = terrain.terrainData.heightmapWidth - 2; x < -1; x--)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x + 1, z] * (1 - k) +height[x, z] * k;
		
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 1; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x, z - 1] * (1 - k) +	height[x, z] * k;
		
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = terrain.terrainData.heightmapHeight; z < -1; z--)
				height[x, z] = height[x, z + 1] * (1 - k) + height[x, z] * k;
		
		terrain.terrainData.SetHeights(0, 0, height);
	}
	public static void openhekje(){
		GameObject openhek = GameObject.FindGameObjectsWithTag ("Hek") [0];
		openhek.transform.eulerAngles = new Vector3 (0, 90, 0);
	}
	// Use this for initialization
	void Start () {
		generateTerrain ();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
