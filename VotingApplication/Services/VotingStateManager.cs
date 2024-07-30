namespace VotingApplication.Services;

public class VotingStateManager
{
    private const string HAS_NOT_VOTED_FLAG = "x";
    private const string HAS_VOTED_FLAG = "v";
    private const int ZERO_VOTES_VALUE = 0;
    
    private readonly Dictionary<string, string> _voters = new();
    private readonly Dictionary<string, int> _candidates = new();

    public event Func<Task> OnCandidatesChange;
    public event Func<Task> OnVotersChange;

    public IReadOnlyDictionary<string, int> Candidates => _candidates.AsReadOnly();
    public IReadOnlyDictionary<string, string> Voters => _voters.AsReadOnly();

    public void AddNewCandidate(string newCandidate)
    {
        _candidates.Add(newCandidate, ZERO_VOTES_VALUE);
        OnCandidatesChange.Invoke();
    }
    
    public void AddNewVoter(string newVoter)
    {
        _voters.Add(newVoter, HAS_NOT_VOTED_FLAG);
        OnVotersChange.Invoke();
    }
    
    public void UpdateVotes(string candidate, string voter)
    {
        _candidates[candidate]++;
        _voters[voter] = HAS_VOTED_FLAG;
        OnCandidatesChange.Invoke();
        OnVotersChange.Invoke();
    }
}