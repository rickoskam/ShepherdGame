using UnityEngine;
using System.Collections;

public class Herder : MonoBehaviour {
	public Terrain terrain;
	private TerrainData terraindata;

	float speed = 30;
	float rotatespeed = 100;
	public float breedte;
	public float lengte ;
	public int aantalHeuvels;
	int aantalSloten = 10;
	
	void editTerrain(){
		int heightmapWidth = terraindata.heightmapWidth-2;
		int heightmapHeigth = terraindata.heightmapHeight-2;
		print (heightmapWidth);
		float beginhoogte = 0.01f;
		float [,] heights = new float[heightmapWidth,heightmapHeigth];
		for (int x = 0; x < heightmapWidth; x++) {
			for(int z = 0; z < heightmapHeigth; z++){
				heights[x,z] = beginhoogte;
			}
		}
		terraindata.SetHeights(0,0,heights);

		ArrayList posities = new ArrayList ();
		//Genereer heuvels
		for (int i = 0; i < aantalHeuvels; i++) {
			int randomx = (int)(Random.value*heightmapWidth);
			int randomz = (int)(Random.value*heightmapHeigth);
			float[] positie = {randomx,randomz};
			posities.Add(positie);
			float randomheight = Random.value* 0.03f;
			float randomhillwidth = (int)(Random.value*100);
			float randomhillength = (int)(Random.value*100);
			float straal = Random.Range(0,50);
			//for(int a = 0; a < 2*Mathf.PI; a++){
			//	for(int r = 0; r < straal; r++){
			//		int x = (int)(r*Mathf.Cos(a));
			//		int z = (int)(r*Mathf.Sin(a));
			//		print (9999);
			//		print (heightmapWidth);
			//		print (Mathf.Min(heightmapWidth,x));
			//		heights[Mathf.Min(heightmapWidth,x,Mathf.Min(heightmapHeigth,z))] = randomheight;
			//	}

			for(int j = 0; j < randomhillwidth; j++){
				for(int k = 0; k < randomhillength; k++){
					heights[511,511] = randomheight;
				}
			}
		}
		//Genereer sloten
		for (int i = 0; i < 5; i++) {
			int beginx = (int)(Random.value*heightmapWidth);
			int beginz = (int)(Random.value*heightmapHeigth);
			int slootlengte = (int)(Random.value*100);
			int slootbreedte = (int)(Random.value*100);
			for(int j = 0; j < slootlengte; j++){
				for(int k = 0; k < slootbreedte; k++){
					heights[511,511]= 0;
				}
			}
		}
		terraindata.SetHeights (0, 0, heights);
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
	void generateLevel(float lengte, float breedte){
		terraindata = terrain.terrainData;
		editTerrain ();
		Smooth ();
		Smooth ();
		Smooth ();
		Smooth ();
		Smooth ();
		Smooth ();
		Smooth ();
		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		GameObject grondPrefab = Resources.Load ("Ground") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer>().bounds.size.x;
		GameObject grond = Instantiate (grondPrefab);
		grond.transform.localScale = new Vector3(lengte,(float)0.000001, breedte);
		grond.transform.position = new Vector3 ((float)0.5*lengte, -5, (float)0.5*breedte);
		for (int i = 1; i<(lengte/lengteHek)+1; i++) {
			GameObject go = Instantiate(hekPrefab);
			go.transform.position=new Vector3(lengteHek*i,0,0);
		}
		for (int i = 0; i<(breedte/lengteHek); i++) {
			GameObject go = Instantiate (hekPrefab);
			go.transform.position = new Vector3(0,0,lengteHek*i);
			go.transform.eulerAngles = new Vector3(0,90,0);
		}
		for (int i = 0; i<(lengte/lengteHek); i++) {
			GameObject go = Instantiate(hekPrefab);
			go.transform.position=new Vector3(lengte-lengteHek*i,0,breedte);
		}
		for (int i = 1; i<(breedte/lengteHek)+1; i++) {
			GameObject go = Instantiate (hekPrefab);
			go.transform.position = new Vector3(lengte,0,breedte-lengteHek*i);
			go.transform.eulerAngles = new Vector3(0,90,0);
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(new Vector3(0,rotatespeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(new Vector3(0,-rotatespeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
	}
}
