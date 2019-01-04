using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelScenesBean  {

    public long id;
    //等级
    public int level;
    //物品初始销售价格
    public double goods_sell_price;
    //场地初始销售价格
    public double space_sell_price;
    //物品初始增长
    public double item_grow;
    //商品名称
    public string goods_name;
    //场地名称
    public string space_name;

    //商品简介
    public string goods_introduction;
    //地皮简介
    public string space_introduction;
}
