using UnityEngine;
using UnityEngine.Experimental.Director;

public class PlayableAPITest : MonoBehaviour
{
    public Animator PlayerAnimatorController;
    public Animator WeaponAnimatorController;
    public float weight;

    private PlayableGraph playableGraph;
    private PlayableHandle mixer;

    void Start()
    {
        mixer = AnimationPlayableUtilities.PlayMixer(GetComponent<Animator>(), 2, out playableGraph);
        playableGraph.Connect(playableGraph.CreateAnimatorControllerPlayable(PlayerAnimatorController.runtimeAnimatorController), 0, mixer, 0);
        playableGraph.Connect(playableGraph.CreateAnimatorControllerPlayable(WeaponAnimatorController.runtimeAnimatorController), 0, mixer, 1);
        GraphVisualizerClient.Show(playableGraph, name);
    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixer.SetInputWeight(0, 1.0f - weight);
        mixer.SetInputWeight(1, weight);
    }

    void OnDisable()
    {
        // Destroys all Playables and Outputs created by the graph.
        playableGraph.Destroy();
    }

}