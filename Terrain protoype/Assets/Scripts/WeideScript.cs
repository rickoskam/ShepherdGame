using UnityEngine;
using System.Collections;

public class WeideScript : MonoBehaviour {
	public Terrain terrain;
	public int levelgrootte;
	public int aantalHeuvels;
	public int aantalVijvers;
	public int aantalBergen;
	public int aaantalSloten;
	public int aantalMeren;
	void generateTerrain(){
		TerrainData terraindata = terrain.terrainData;
		int heightmapx = terraindata.heightmapWidth;    
		int heightmapz = terraindata.heightmapHeight;
		float [] positionsx = new float[aantalHeuvels];
		float[] positionsz = new  float[aantalHeuvels];
		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		GameObject Schaap = Resources.Load ("sheep") as GameObject;
		GameObject Steen = Resources.Load ("RockMesh") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer>().bounds.size.x;
		int levelsizex = (int)(levelgrootte*lengteHek);  
		int levelsizez = (int)(levelgrootte*lengteHek);
		float beginhoogte = 0.008f;
		float[,] heights = new float[heightmapx, heightmapz];
		float terrainsizex = terraindata.size.x;
		float terrainsizez = terraindata.size.z;
		float verhoudingx = levelsizex / terrainsizex;
		float verhoudingz = levelsizez / terrainsizez;
		float beginx = 0.5f * (float)terrainsizex - 0.5f * (float)levelsizex;
		float eindx  = 0.5f * (float)terrainsizex + 0.5f * (float)levelsizex;
		float beginz = 0.5f * (float)terrainsizez - 0.5f * (float)levelsizez;
		float eindz  = 0.5f * (float)terrainsizez + 0.5f * (float)levelsizez;
		int hmlevellengthx = (int)(heightmapx * verhoudingx);
		int hmlevellengthz = (int)(heightmapz * verhoudingz);
		int hmlevelbeginx  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthx);
		int hmleveleindx   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthx);
		int hmlevelbeginz  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthz);
		int hmleveleindz   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthz);

		// set beginhoogte
		for (int x = 0; x < heightmapx; x++) {
			for(int z = 0; z < heightmapz; z++){
				heights[x,z] = beginhoogte;
			}
		}
		terraindata.SetHeights(0,0,heights);		
		// heuvels genereren in het level
		for (int i = 0; i < aantalHeuvels; i++) {
			int minradius = (int)(terrainsizex/55);
			int maxradius = (int)(terrainsizex/40);
			int heuvelradius = (int) (Random.Range(minradius,maxradius));
			int xbegin = (int) (Random.Range (hmlevelbeginx+maxradius,hmleveleindx-maxradius));
			int zbegin = (int) (Random.Range (hmlevelbeginz+maxradius,hmleveleindz-maxradius));
			positionsx[i] = xbegin;
			positionsz[i] = zbegin;
			float top = 0.04f;
			float helling = (float)(top*(0.01f/0.05f));
			for(int r = 1; r < heuvelradius; r++){
				for(int d = 0; d < 360; d++){
					int x = xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int z = zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					float hoogte = Mathf.Max ((float)(top-(Mathf.Pow((r*helling),2))),(float)(beginhoogte*1.1));
					if(heights[Mathf.Max(x,0),Mathf.Max(z,0)]<hoogte){
						heights[Mathf.Max(x,0),Mathf.Max(z,0)] = hoogte;
					}
				}
			}
		}

		// vijvers genereren level
		for (int i = 0; i < aantalVijvers; i++) {
			int minradius = (int)(terrainsizex/90);
			int maxradius = (int)(terrainsizex/80);
			int vijverradius = (int) (Random.Range(minradius,maxradius));
			int xbegin = (int) (Random.Range (hmlevelbeginx+maxradius,hmleveleindx-maxradius));
			int zbegin = (int) (Random.Range (hmlevelbeginz+maxradius,hmleveleindz-maxradius));
			for(int r = 1; r < vijverradius; r++){
				for(int d = 0; d < 360; d++){
					int x = xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int z = zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180));

					if(positioncheck(x, z, positionsx, positionsz)){
						heights[x,z] = 0;
					}
				}
			}
		}
		// meren genereren omgeving
		for (int i = 0; i < aantalMeren; i++) {
			int minradius = (int)(terrainsizex/60);
			int maxradius = (int)(terrainsizex/40);
			int meerradius = (int) (Random.Range(minradius,maxradius));
			int xbegin = (int) (Random.Range (hmleveleindx+maxradius,heightmapx-maxradius));
			int zbegin = (int) (Random.Range (hmleveleindz+maxradius,heightmapz-maxradius));
			for(int r = 1; r < meerradius; r++){
				for(int d = 0; d < 360; d++){
					int x = xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int z = zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					heights[x,z] = 0;
				}
			}
		}
		// bergen genereren omgeving
		for(int i = 0; i < aantalBergen; i++){
			int xbegin = (int)(Random.Range(0,hmlevelbeginx*0.5f));
			int zbegin = (int)Random.Range(0,heightmapz);
			int minradius = (int)(terrainsizex/30);
			int maxradius = (int)(terrainsizex/20);
			int bergradius = (int) (Random.Range(minradius,maxradius));
			float top = Random.Range(0.2f,1)*0.06f*(terrainsizex/2000);
			float helling = (float)(top*(0.001f/0.03f));
			for(int r = 1; r < bergradius; r++){
				for(int d = 0; d < 360; d++){
					int x = Mathf.Min(xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180)),(int)hmlevelbeginx);
					int z = Mathf.Min(zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180)),(int)heightmapz);
					try{if(heights[x,z]>beginhoogte){
							float hoogte = Random.value*0.3f - Random.value*0.006f*r + Random.Range(-0.03f,0.03f);
							heights[Mathf.Max(x,0),Mathf.Max(z,0)] = Mathf.Max (hoogte,heights[x,z]);
						}
						else{
							float hoogte = 0.3f - 0.006f*r;
							heights[Mathf.Max(x,0),Mathf.Max(z,0)] = Mathf.Max(hoogte,heights[x,z]);
						}
					}
					catch(System.IndexOutOfRangeException){

					}
				}
			}
		}
		for(int i = 0; i < aantalBergen; i++){
			int xbegin = (int)Random.Range(0,heightmapx);
			int zbegin = (int)Random.Range(0,hmlevelbeginz*0.5f);
			int minradius = (int)(terrainsizex/30);
			int maxradius = (int)(terrainsizex/20);
			int bergradius = (int) (Random.Range(minradius,maxradius));
			float top = Random.Range(0.2f,1)*0.06f*(terrainsizex/2000);
			float helling = (float)(top*(0.001f/0.03f));
			for(int r = 1; r < bergradius; r++){
				for(int d = 0; d < 360; d++){
					int x = Mathf.Min(xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180)),(int)heightmapx);
					int z = Mathf.Min(zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180)),(int)hmlevelbeginz);
					try{if(heights[x,z]>beginhoogte){
							float hoogte = Random.value*0.3f - Random.value*0.006f*r + Random.Range(-0.03f,0.03f);
							heights[Mathf.Max(x,0),Mathf.Max(z,0)] = Mathf.Max (hoogte,heights[x,z]);
						}
						else{
							float hoogte = 0.3f - 0.006f*r;
							heights[Mathf.Max(x,0),Mathf.Max(z,0)] = Mathf.Max(hoogte,heights[x,z]);
						}
					}
					catch(System.IndexOutOfRangeException){
						
					}
				}
			}
		}

		//hekjes spawnen
		int aantalhekjesx = (int)(levelsizex / lengteHek);
		int aantalhekjesz = (int)(levelsizez / lengteHek);

		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek = Instantiate(hekPrefab);
			float posx = beginx + i * lengteHek;
			float posz = beginz;
			hek.transform.position = new Vector3 (posx, Terrain.activeTerrain.SampleHeight(new Vector3(posx,0,posz)) 
			                                      + Terrain.activeTerrain.transform.position.y, posz);
		}
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, terrain.SampleHeight(new Vector3(0,0,0)), eindz);
			if(i == (int)(aantalhekjesx/2)){
				hek2.tag = "Hek";
			}
		}
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, terrain.SampleHeight(new Vector3(0,0,0)), eindz+2*lengteHek);
			GameObject schaap = Instantiate (Schaap);
			schaap.transform.position = new Vector3 (beginx + i * lengteHek,terrain.SampleHeight(new Vector3(0,0,0)), eindz + lengteHek);
			schaap.transform.eulerAngles = new Vector3(0,Random.value*360,0);
		}
		for (int i = 0; i < aantalhekjesz+3; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (beginx, terrain.SampleHeight(new Vector3(0,0,0)), beginz+ i*lengteHek);
			hek.transform.eulerAngles = new Vector3(0,90,0);
		}
		for (int i = 0; i < aantalhekjesz+3; i++) {
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (eindx, terrain.SampleHeight(new Vector3(0,0,0)), beginz + i*lengteHek);
			hek2.transform.eulerAngles = new Vector3(0,90,0);
		}
		//stenen spawnen
		for(int i = 0; i<3;i++){
		GameObject steen = Instantiate(Steen);
			steen.transform.position = new Vector3(Random.Range(0,terrainsizex),1000,Random.Range(0,terrainsizez));
			RaycastHit test;
			Ray ray = new Ray(steen.transform.position, Vector3.down);
			if (Physics.Raycast(ray, out test)) {
				steen.transform.Translate(new Vector3(0,-test.distance,0));
			}
		}
			terraindata.SetHeights (0, 0, heights);
		for (int i = 0; i < 4; i++) {
			Smooth();
		}
	}

	public bool positioncheck(float x, float z, float[] xh, float[] zh){
		for(int i = 0; i < aantalHeuvels; i++){
			float disx = x - xh[i];
			float disz = z - zh[i];
			float distance = Mathf.Sqrt(disx*disx + disz*disz);
			if(distance<15){
				return false;
			}
		}
		return true;
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
