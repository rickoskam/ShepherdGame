using UnityEngine;
using System.Collections;

public class TerrainGeneration : MonoBehaviour {
	public Terrain terrain;
	private TerrainData terraindata;
	public int aantalHeuvels;
	public int aantalSloten;
	public int aantalVijvers;
	public int aantalBomen;
	public int aantalBosjes;
	public int levelsizex = 400;
	public int levelsizez = 400;
	public float[] positionsx;
	public float[] positionsz;

	public void GenerateTerrain(){
		terraindata = terrain.terrainData;
		float [] positionsx = new float[aantalHeuvels];
		float[] positionsz = new  float[aantalHeuvels];

		int heightmapwidth = terraindata.heightmapWidth-1;
		int heightmapheigth = terraindata.heightmapHeight - 1;

		float beginhoogte = 0.003f;
		float[,] heights = new float[heightmapwidth + 1, heightmapheigth + 1];

		for (int x = 0; x < heightmapwidth; x++) {
			for(int z = 0; z < heightmapheigth; z++){
				heights[x,z] = beginhoogte;
			}
		}

		terraindata.SetHeights(0,0,heights);

		// heuvels genereren
		for (int i = 0; i < aantalHeuvels; i++) {
			int beginx = (int) (Random.value*heightmapwidth);
			int beginz = (int) (Random.value*heightmapheigth);
			positionsx[i] = beginx;
			positionsz[i] = beginz;
			int heuvelradius = (int) (Random.Range(70,100));
			float top = 0.03f;
			float helling = (float)(top*(0.002f/0.03f));
			for(int r = 1; r < heuvelradius; r++){
				for(int d = 0; d < 360; d++){
					int xx = beginx + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int x = Mathf.Min(xx,heightmapwidth);
					int zz = beginz + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					int z = Mathf.Min(zz,heightmapheigth);
					float hoogte = Mathf.Max ((float)(top-(Mathf.Pow((r*helling),2))),(float)(beginhoogte*1.1));
					if(heights[Mathf.Max(x,0),Mathf.Max(z,0)]<hoogte){
						heights[Mathf.Max(x,0),Mathf.Max(z,0)] = hoogte;
					}
				}
			}
		}
		// sloten genereren
		for (int i = 0; i < aantalSloten; i++) {
			float randomx = Random.value;
			float randomz = Random.value;
			int beginx = (int) (randomx*(float)heightmapwidth);
			int beginz = (int) (randomz*(float)heightmapheigth);
			float realx = ((float)beginx/(float)heightmapwidth)*terraindata.size.x;
			float realz = ((float)beginz/(float)heightmapheigth)*terraindata.size.z;
			int slootbreedte = (int)(Random.Range(50,70));
			int slootlengte = (int)(Random.Range(300,400));
			GameObject gras = Resources.Load ("FernMesh") as GameObject;
			float lengteGras = gras.GetComponent<Renderer>().bounds.size.x;
			if(Random.value>0){
				for(int x = 0; x < slootlengte; x++){
					for(int z = 0; z < slootbreedte; z++){
						int xx = Mathf.Min((beginx + x),heightmapwidth);
						int zz = Mathf.Min((beginz + z),heightmapheigth);
						if(positioncheck(xx, zz, positionsx, positionsz)){
							heights[Mathf.Max(xx,0), Mathf.Max (zz,0)] = 0;
						}
					}
				}
				//STAAT UIT
				for(int k = 0; k < 10; k++){
					if(false){
						GameObject nieuwgras = Instantiate (gras);
						nieuwgras.transform.position = new Vector3(realz, 2, realx + Random.value*k*lengteGras);
						nieuwgras.transform.localScale = new Vector3(500,500,500);
						nieuwgras.transform.eulerAngles = new Vector3(0,Random.value*Mathf.PI,0);
					}
				}
			}
			else{
				for(int x = 0; x < slootbreedte; x++){
					for(int z = 0; z < slootlengte; z++){
						int xx = Mathf.Min((beginx + x),heightmapwidth);
						int zz = Mathf.Min((beginz + z),heightmapheigth);
						if(positioncheck(xx, zz, positionsx, positionsz)){
							heights[Mathf.Max(xx,0), Mathf.Max (zz,0)] = 0;
						}					
					}
				}
			}
		}
		// vijvers genereren
		for (int i = 0; i < aantalVijvers; i++) {
			int beginx = (int) (Random.value*heightmapwidth);
			int beginz = (int) (Random.value*heightmapheigth);
			int vijverradius = (int) (Random.Range(30,70));
			for(int r = 1; r < vijverradius; r++){
				for(int d = 0; d < 360; d++){
					int xx = beginx + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int x = Mathf.Min(xx,heightmapwidth);
					int zz = beginz + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					int z = Mathf.Min(zz,heightmapheigth);
					if(positioncheck(x,z,positionsx, positionsz)){	
						heights[Mathf.Max(x,0),Mathf.Max(z,0)] = 0;
					}
				}
			}

		}

		terraindata.SetHeights (0, 0, heights);

		for (int i = 0; i < 5; i++) {
			Smooth();
		}

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
	public bool positioncheck(float x, float z, float[] xh, float[] zh){
		for(int i = 0; i < aantalHeuvels; i++){
			float disx = x - xh[i];
			float disz = z - zh[i];
			float distance = Mathf.Sqrt(disx*disx + disz*disz);
			if(distance<80){
					return false;
			}
		}
		return true;
	}
	void GenerateTrees(){
		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer>().bounds.size.x*0.95f;
		int aantalhekjesx = (int)(terraindata.size.x / lengteHek);
		int aantalhekjesz = (int)(terraindata.size.z / lengteHek);
		for (int i = 1; i < aantalhekjesx+1; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (0+ i*lengteHek, terrain.transform.position.y, 0);
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (0+i*lengteHek, terrain.transform.position.y, terraindata.size.z);
		}
		for (int i = 0; i < aantalhekjesz+1; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (0, terrain.transform.position.y, 0+ i*lengteHek);
			hek.transform.eulerAngles = new Vector3(0,90,0);
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (terraindata.size.z, terrain.transform.position.y, i*lengteHek);
			hek2.transform.eulerAngles = new Vector3(0,90,0);
		}
		
		GameObject bosje = Resources.Load ("sycamore") as GameObject;
		GameObject boom = Resources.Load ("sycamore") as GameObject;
		
		for(int i = 0; i < aantalBosjes; i++){
			GameObject bosje1 = Instantiate(bosje);
			bosje1.transform.position = new Vector3((float)(Random.value*terraindata.size.x),-10,(float)(Random.value*terraindata.size.z));
			RaycastHit test;
			Ray testray = new Ray(bosje1.transform.position, Vector3.down);
			if (Physics.Raycast(testray, out test)) {
				bosje1.transform.Translate(new Vector3(0,-test.distance,0));
			}
		}
		
		for(int i = 0; i < aantalBomen; i++){
			GameObject boom1 = Instantiate(boom);
			boom1.transform.position = new Vector3((float)(Random.value*terraindata.size.x),-10,(float)(Random.value*terraindata.size.z));
			RaycastHit test;
			Ray testray = new Ray(boom1.transform.position, Vector3.down);
			if (Physics.Raycast(testray, out test)) {
				boom1.transform.Translate(new Vector3(0,-test.distance,0));
				boom1.transform.localScale = new Vector3(500,500,500);
			}
		}
	}

	// Use this for initialization
	void Start () {
		GenerateTerrain ();
		GenerateTrees ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}