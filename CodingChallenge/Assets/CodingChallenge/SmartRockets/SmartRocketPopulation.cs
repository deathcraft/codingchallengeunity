using Boo.Lang;
using UnityEngine;

public class SmartRocketPopulation : MonoBehaviour
{
    public int rocketNum;
    public GameObject rocketPrefab;

    public float lifetimeSec = 20f;
    public float timeScale = 2;

    private List<SmartRocket> rockets = new List<SmartRocket>();
    private List<SmartRocket> matingPool = new List<SmartRocket>();
    private float elapsed;

    private void Start()
    {
        lifetimeSec = lifetimeSec * timeScale;
        GeneratePopulation();
    }

    private void GeneratePopulation()
    {
        rockets.Clear();

        for (int i = 0; i < rocketNum; i++)
        {
            SmartRocket.DNA dna = new SmartRocket.DNA((int) ((int) lifetimeSec));
            InstantiateRocket(dna);
        }
    }

    private void InstantiateRocket(SmartRocket.DNA dna)
    {
        var instance = Instantiate(rocketPrefab);
        instance.transform.position = transform.position;
        var smartRocket = instance.GetComponent<SmartRocket>();

        if (dna != null)
        {
            smartRocket.SetDNA(dna);
        }
        
        rockets.Add(smartRocket);
    }

    private void Evaluate()
    {
        matingPool.Clear();
        float maxFit = 0;

        foreach (SmartRocket smartRocket in rockets)
        {
            smartRocket.CalculateFitness();
            if (smartRocket.fitness > maxFit)
            {
                maxFit = smartRocket.fitness;
            }
        }
        Debug.Log("maxFit: " + (maxFit));

        foreach (SmartRocket smartRocket in rockets)
        {
            smartRocket.fitness /= maxFit;
        }

        foreach (SmartRocket smartRocket in rockets)
        {
            float n = smartRocket.fitness * 100;

            for (int j = 0; j < n; j++)
            {
                matingPool.Add(smartRocket);
            }
        }

    }

    private void Selection()
    {
        List<SmartRocket.DNA> newDNAs = new List<SmartRocket.DNA>();

        foreach (var smartRocket in rockets)
        {
            int id1 = Random.Range(0, matingPool.Count);
            int id2 = Random.Range(0, matingPool.Count);
            SmartRocket.DNA partnerAdna = matingPool[id1].dna;
            SmartRocket.DNA partnerBdna = matingPool[id2].dna;

            SmartRocket.DNA childDNA = partnerAdna.Crossover(partnerBdna);
            newDNAs.Add(childDNA);
        }

        foreach (var smartRocket in rockets)
        {
            Destroy(smartRocket.gameObject);
        }
        
        rockets.Clear();

        foreach (var newDnA in newDNAs)
        {
            InstantiateRocket(newDnA);
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime* timeScale;

        if (elapsed >= lifetimeSec)
        {
            NewGeneration();
            return;
        }
        
        foreach (var smartRocket in rockets)
        {
            smartRocket.Tick((int) elapsed);
        }
    }

    public void NewGeneration()
    {
        Evaluate();
        Selection();
        elapsed = 0;
    }
}