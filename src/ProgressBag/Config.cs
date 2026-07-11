using LazyAPI.Attributes;
using LazyAPI.ConfigFiles;
using LazyAPI.Utility;

namespace ProgressBag;

public class Award
{
    public int netID = 22;

    public int prefix = 0;

    public int stack = 99;
}

public class Bag
{
    [LocalizedPropertyName(CultureType.Chinese, "礼包名称")]
    [LocalizedPropertyName(CultureType.English, "Name")]
    public string Name { get; set; } = "新手礼包";

    [LocalizedPropertyName(CultureType.Chinese, "进度限制")]
    [LocalizedPropertyName(CultureType.English, "ProgressLimit")]
    public List<string> Limit { get; set; } = new();

    [LocalizedPropertyName(CultureType.Chinese, "礼包奖励")]
    [LocalizedPropertyName(CultureType.English, "Award")]
    public List<Award> Award { get; set; } = new();

    [LocalizedPropertyName(CultureType.Chinese, "执行命令")]
    [LocalizedPropertyName(CultureType.English, "Commands")]
    public List<string> Command { get; set; } = new();

    [LocalizedPropertyName(CultureType.Chinese, "已领取玩家")]
    [LocalizedPropertyName(CultureType.English, "ReceivePlayers")]
    public List<string> Receive { get; set; } = new();

    [LocalizedPropertyName(CultureType.Chinese, "可领取组")]
    [LocalizedPropertyName(CultureType.English, "Groups")]
    public List<string> Group { get; set; } = new();
}

[Config]
public class Config : JsonConfigBase<Config>
{
    [LocalizedPropertyName(CultureType.Chinese, "礼包")]
    [LocalizedPropertyName(CultureType.English, "Bags")]
    public List<Bag> Bag = new();

    protected override void SetDefault()
    {
        var bagIds = new Dictionary<string, int>
        {
            {"史莱姆王", 3318}, {"克苏鲁之眼", 3319}, {"世界吞噬怪", 3320},
            {"克苏鲁之脑", 3321}, {"蜂王", 3322}, {"骷髅王", 3323},
            {"血肉墙", 3324}, {"毁灭者", 3325}, {"双子魔眼", 3326},
            {"机械骷髅王", 3327}, {"世纪之花", 3328}, {"石巨人", 3329},
            {"猪龙鱼公爵", 3330}, {"拜月教邪教徒", 3331}, {"月亮领主", 3332},
            {"双足翼龙", 3860}, {"光之女皇", 4782}, {"史莱姆皇后", 4957},
            {"独眼巨鹿", 5111}
        };
        foreach (var (name, _) in GameProgress.DefaultProgressNames)
        {
            Bag bag = new();
            bag.Limit.Add(name);
            bag.Name = name + "宝藏袋";
            bag.Award.Add(new Award
            {
                netID = bagIds.TryGetValue(name, out var id) ? id : 22,
                stack = 1,
                prefix = 0
            });
            this.Bag.Add(bag);
        }
    }

    public void Reset()
    {
        foreach (var bag in this.Bag)
        {
            bag.Receive.Clear();
        }
        this.SaveTo();
    }
}
