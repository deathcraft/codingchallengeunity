using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SmartRocket : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    public float fitness;

    private Vector3 previousPosition;
    public DNA dna;
    private SmartTarget target;
    
    void Start()
    {
        target = FindObjectOfType<SmartTarget>();
    }

    private void LookForward()
    {
        var currentPosition = this.transform.position;
        transform.up = rigidbody.velocity;
        previousPosition = currentPosition;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 0.5f)
        {
            rigidbody.velocity = Vector3.zero;
        }    
     }

    public void Tick(int tick)
    {
        rigidbody.velocity = dna.CurrentForce(tick);
    }

    public void SetDNA(DNA newDNA)
    {
        this.dna = newDNA;
    }
    
    public void CalculateFitness()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        fitness = 1 / (dist);

        if (dist < 0.1f)
        {
            fitness = 1000;
        }
    }

    public class DNA
    {
        private readonly int lifespan;
        private Vector3[] genes;

        public DNA(int lifespan, bool generateGenes = true)
        {
            this.lifespan = lifespan;
            genes = new Vector3[lifespan];

            if (generateGenes)
            {
                for (int i = 0; i < lifespan; i++)
                {
                    genes[i] = Random.insideUnitCircle * Random.Range(5, 10f);
                }                
            }
        }

        public Vector3 CurrentForce(float elapsed)
        {
            var id = (int) elapsed;
            return genes[id];
        }

        public DNA Crossover(DNA otherDna)
        {
            DNA newDNA = new DNA(lifespan, false);
            int mid = Random.Range(0, lifespan);

            for (int i = 0; i < genes.Length; i++)
            {
                if (i < mid)
                {
                    newDNA.genes[i] = this.genes[i];
                }
                else
                {
                    newDNA.genes[i] = otherDna.genes[i];
                }
            }

            for (int i = 0; i < newDNA.genes.Length; i++)
            {
                if (Random.value < 0.01f)
                {
                    newDNA.genes[i] = Random.insideUnitCircle * Random.Range(5, 10f);
                }
            }

            return newDNA;
        }
    }

   
}