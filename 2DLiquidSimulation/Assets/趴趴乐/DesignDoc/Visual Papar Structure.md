# Visual Papar Structure

## 1. 文档目标
本文档用于定义单个“趴趴”在 Unity 中的视觉 Prefab 结构，重点说明：
- 正面朝向 Prefab 结构
- 背面朝向 Prefab 结构
- 盒体与宠物的遮挡关系
- 建议的 Sorting Layer 与 Sorting Order
- 动画时的分层处理原则

本文档服务于程序实现、美术拆层和动画制作，目标是让“趴趴”在不同朝向下都能稳定显示，并且后续做轻量动画时不会频繁出现遮挡错误。

---

## 2. 总体原则

## 2.1 盒体负责固定遮挡
盒体结构中的前框、后框、顶面、底面、侧面应尽量承担固定遮挡职责。

也就是说：
- 哪些内容永远在盒体前面
- 哪些内容永远在盒体后面
- 哪些内容永远在盒子内部

这些关系应由 Prefab 结构与 Sorting Order 预先确定，而不是运行时动态计算。

## 2.2 宠物负责局部切层
宠物不建议作为一张整图处理。
建议拆成以下三层：
- PetBack
- PetBody
- PetFront

必要时再细分到耳朵、尾巴、手脚。

这样做的目的：
- 普通待机动画只改 Transform
- 遮挡变化只影响局部部件
- 避免整只宠物在动画中频繁改排序

## 2.3 动画优先改姿势，不优先改排序
普通动画优先做：
- 位移
- 旋转
- 缩放

只有在前后关系真的变化时，才切换局部部件的显示层，或者启用前后两套部件。

---

## 3. 统一 Sorting 规则

## 3.1 推荐 Sorting Layer
所有“趴趴”内部 SpriteRenderer 建议统一使用同一个 Sorting Layer，例如：

`Papar`

不同实例之间的整体前后关系，可在实例根节点上再做更高层级排序控制。

## 3.2 单个趴趴内部建议 Sorting Order
以下为单个趴趴内部从后到前的推荐顺序：

| 层级 | 名称 | Sorting Order | 说明 |
|------|------|---------------|------|
| 1 | FrameBack | 0 | 边框后层，位于宠物后面 |
| 2 | Bottom | 5 | 底面 |
| 3 | InnerSide | 10 | 盒体内部侧面/背景 |
| 4 | PetBack | 20 | 宠物后层，如后耳、后尾、后脚 |
| 5 | PetBody | 30 | 宠物主体层 |
| 6 | FaceOverlay | 35 | 五官或头部附加层 |
| 7 | PetFront | 40 | 前爪、前耳、探出盒外局部 |
| 8 | FrameFront | 50 | 边框前层 |
| 9 | Top | 60 | 顶面 |
| 10 | OuterSide | 70 | 外部侧面，最外层结构 |

说明：
- `FrameFront` 需要稳定压住宠物主体的边缘，但不能遮住那些明确设计为“探出盒外”的前爪或前手
- `Top` 与 `OuterSide` 通常作为盒体最外层结构，不建议在动画中切换
- `FaceOverlay` 可选，如果五官单独拆层时使用

---

## 4. 正面朝向 Prefab 结构

## 4.1 正面朝向显示目标
正面朝向的目标是强调：
- 头部朝向玩家
- 五官清晰可见
- 前爪可以探出盒口
- 角色具有“看着你”的互动感

## 4.2 推荐 Prefab 层级

```text
PaparInstance_Front
|- BoxBack
|  |- FrameBack
|  |- Bottom
|  |- InnerSide
|- Pet
|  |- PetBack
|  |  |- LeftEarBack
|  |  |- RightEarBack
|  |  |- TailBack
|  |- PetBody
|  |  |- Body
|  |  |- Head
|  |  |- HeadRightEar
|  |  |- HeadLeftEar
|  |- FaceOverlay
|  |  |- Eyes
|  |  |- Nose
|  |  |- Mouth
|  |- PetFront
|     |- LeftPawFront
|     |- RightPawFront
|- BoxFront
|  |- FrameFront
|  |- Top
|  |- OuterSide
```

## 4.3 正面朝向分层建议

### BoxBack
- `FrameBack`
  用于提供边框的后层厚度感
- `Bottom`
  表示宠物坐落的底面
- `InnerSide`
  表示盒体内部背景

### PetBack
用于放置那些默认在身体后方的内容：
- 后耳
- 后尾巴
- 某些缩在身体后的脚

对于正面朝向，`PetBack` 通常存在感较弱，可尽量简化。

### PetBody
这是正面朝向最核心的一层：
- 头部主体
- 身体主体
- 耳朵主体

如果耳朵需要稳定压在头部之上，可继续放在这一层内部用节点顺序控制。

### FaceOverlay
五官建议单独一层，理由：
- 方便替换表情
- 方便做眨眼和嘴部微动
- 不影响头部主轮廓排序

### PetFront
用于放置那些明确探出盒口的内容：
- 左前爪
- 右前爪

如果后续有“探头”“伸手”“前扑”之类动作，也应优先把外探局部放在这一层。

### BoxFront
正面朝向下的盒体前层：
- `FrameFront` 负责形成开口边框
- `Top` 压住内部后方内容
- `OuterSide` 负责最外层侧面体积

---

## 5. 背面朝向 Prefab 结构

## 5.1 背面朝向显示目标
背面朝向的目标是强调：
- 屁屁和尾巴的可爱感
- 后腿或脚掌的展示
- 身体重心偏内部，但局部仍可贴近盒口

背面朝向与正面朝向最大的区别不是“换一张图”，而是：
- 尾巴和屁股成为主视觉重点
- 耳朵和头部的显示逻辑需要后移
- 手脚的前后层关系会重新分配

## 5.2 推荐 Prefab 层级

```text
PaparInstance_Back
|- BoxBack
|  |- FrameBack
|  |- Bottom
|  |- InnerSide
|- Pet
|  |- PetBack
|  |  |- HeadBack
|  |  |- EarBackLeft
|  |  |- EarBackRight
|  |- PetBody
|  |  |- HipBody
|  |  |- ButtMain
|  |  |- TailMain
|  |- PetFront
|     |- LeftFootFront
|     |- RightFootFront
|     |- TailFrontOptional
|- BoxFront
|  |- FrameFront
|  |- Top
|  |- OuterSide
```

## 5.3 背面朝向分层建议

### BoxBack
与正面朝向一致：
- `FrameBack`
- `Bottom`
- `InnerSide`

盒体结构不应因正反面切换而改变。

### PetBack
背面朝向中，头部和耳朵通常后移：
- 头后部
- 左耳后层
- 右耳后层

这些内容通常被身体主体遮住一部分，因此更适合放在 `PetBack`。

### PetBody
背面朝向最重要的主体：
- 臀部主体
- 身体大轮廓
- 尾巴主体

如果尾巴主要位于身体侧后方，可直接放在 `PetBody`。

### PetFront
用于那些贴近盒口、靠前显示的部分：
- 左后脚
- 右后脚
- 某些特殊款尾巴前层

如果某只宠物的尾巴会甩到脚前面，可以单独增加 `TailFrontOptional`，平时关闭，只在特殊动作中启用。

### BoxFront
背面朝向与正面一致：
- `FrameFront`
- `Top`
- `OuterSide`

---

## 6. 正反面共用结构建议

## 6.1 共享盒体，不共享宠物层
建议盒体结构在正面和背面之间共用：
- FrameBack
- Bottom
- InnerSide
- FrameFront
- Top
- OuterSide

宠物部分则建议正反面分别做自己的视觉层级结构。

原因：
- 盒体的遮挡逻辑是稳定的
- 正反面的宠物视觉重点完全不同
- 如果强行共用一套宠物节点，会让动画和排序变复杂

## 6.2 推荐实现方式
建议一个 `PaparInstance` 中保留：
- `PetFrontFacingRoot`
- `PetBackFacingRoot`

同一时刻只启用一个。

结构示意：

```text
PaparInstance
|- BoxBack
|- PetFrontFacingRoot
|- PetBackFacingRoot
|- BoxFront
```

优点：
- 正反面的视觉结构完全独立
- 逻辑上只是切换朝向根节点显隐
- 后续做不同宠物种类时也更容易复用规范

---

## 7. 动画时的遮挡处理建议

## 7.1 首选方案
动画优先改变：
- 局部位置
- 局部旋转
- 局部缩放

普通待机不建议修改 Sorting Order。

例如：
- 呼吸：身体整体轻微缩放
- 眨眼：切换五官贴图
- 尾巴摆动：尾巴节点旋转
- 爪子轻抬：前爪节点轻微上移

## 7.2 需要切层的情况
仅在以下情况建议切层：
- 尾巴从身体后甩到身体前
- 某只耳朵从头后翻到头前
- 爪子从盒内伸到盒外

此时推荐做法不是动态改同一节点的排序，而是：
- 做前后两份部件
- 通过显隐切换或动画事件切换

例如：
- `TailBack`
- `TailFront`

动画过程中切换显示，而不是让一个 `Tail` 节点反复改 `sortingOrder`。

## 7.3 首版动画限制建议
首版优先支持：
- 呼吸
- 眨眼
- 轻微头部摆动
- 轻微爪子抬起
- 轻微尾巴摆动

首版暂不建议支持：
- 整体翻身
- 大范围探出盒口
- 身体与边框大幅穿插
- 高频前后切层动画

这样能显著降低美术与程序复杂度。

---

## 8. 推荐的 Unity 组织方式

## 8.1 单个渲染节点建议
每个可见视觉部件建议：
- 单独一个子节点
- 一个 `SpriteRenderer`
- 固定 `Sorting Layer = Papar`
- 使用明确命名

例如：
- `FrameFront`
- `Top`
- `Body`
- `Eyes`
- `LeftPawFront`

## 8.2 不建议的做法
以下做法不建议作为首版方案：
- 整只宠物只用一张图，然后运行时硬切遮挡
- 大量依赖脚本动态修改全身排序
- 使用自动排序算法推断耳朵/尾巴前后关系
- 盒体结构和宠物结构混在同一层级中

---

## 9. 命名规范建议

### 盒体
- `FrameBack`
- `Bottom`
- `InnerSide`
- `FrameFront`
- `Top`
- `OuterSide`

### 正面宠物
- `Head`
- `Body`
- `Eyes`
- `Nose`
- `Mouth`
- `LeftPawFront`
- `RightPawFront`
- `TailBack`

### 背面宠物
- `HeadBack`
- `HipBody`
- `ButtMain`
- `TailMain`
- `LeftFootFront`
- `RightFootFront`
- `TailFrontOptional`

---

## 10. 总结
《萌宠趴趴乐》的视觉结构应遵循以下原则：

- 盒体结构固定遮挡
- 正反面宠物结构分开组织
- 宠物至少拆成 `PetBack / PetBody / PetFront`
- 普通动画只改姿势，不改排序
- 少数需要前后变化的部件使用前后双版本切换

如果按照这套结构搭建 Prefab：
- 正面和背面都能稳定显示
- 后续增加新宠物时更容易复用
- 动画实现时不会频繁遇到遮挡混乱问题

这份文档可作为后续程序 Prefab 搭建与美术拆层的统一规范。
