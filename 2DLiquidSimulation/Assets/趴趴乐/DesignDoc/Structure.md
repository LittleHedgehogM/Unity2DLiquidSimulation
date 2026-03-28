# 萌宠趴趴乐系统架构

## 1. 文档目标
本文档用于说明《萌宠趴趴乐》首版程序实现的系统架构，作为后续开发时的统一参考。

本文档对应当前版本的 [GDD.md](E:/GitHub/Unity2DLiquidSimulation/2DLiquidSimulation/Assets/趴趴乐/DesignDoc/GDD.md)，重点覆盖：
- 场景结构
- 模式状态机
- 运行时模块划分
- 核心数据结构
- 交互流转
- 首版实现边界

首版目标是做出一个可玩竖切片，优先跑通以下主链路：

`大场景浏览 -> 布局模式 -> 单体编辑模式 -> 返回布局 -> 返回浏览`

---

## 2. 总体设计原则

### 2.1 设计目标
- 优先保证核心交互跑通，而不是一次性做完整内容系统
- 保持新系统独立，不与旧项目业务脚本耦合
- 所有交互围绕“浏览整体陈列、调整布局、编辑单体内容”展开
- 首版以运行时内存数据为主，不引入存档、撤销、资源解锁等额外复杂度

### 2.2 架构原则
- 模式切换集中管理，避免各脚本自行切状态
- 布局系统与单体编辑系统解耦
- 单个“趴趴”作为独立运行时对象，拥有自己的显示与配置
- 数据与表现分离，配置变更通过统一接口驱动视图刷新
- 首版允许使用简化资源和默认数据，但接口设计要能支撑后续扩展

---

## 3. 场景层级结构

## 3.1 场景职责
首版使用一个独立场景承载《萌宠趴趴乐》的完整竖切片。

该场景需要包含以下核心内容：
- 主相机
- 场景根节点
- 趴趴布局容器
- UGUI 画布
- EventSystem
- 模式控制器

## 3.2 推荐场景层级
推荐层级如下：

```text
PaparRoot
|- Main Camera
|- Runtime
|  |- LayoutRoot
|  |- PreviewRoot  
|  |- InteractionRoot
|- UI
|  |- HUDCanvas
|  |  |- TopBar
|  |  |- HoverActions
|  |  |- LayoutHints
|  |  |- SingleEditPanel
|  |- EventSystem
|- Systems
   |- GameModeController
   |- PaparLayoutController
   |- SingleEditController
   |- SelectionService
   |- CameraRigController
   |- PaparFactory
```

### 3.3 层级说明
- `LayoutRoot`
  存放当前所有趴趴实例，是布局操作的核心节点
- `PreviewRoot`
  可用于放置拖拽预览、吸附预览、半透明占位等临时对象
- `InteractionRoot`
  可用于放置新增按钮、接缝提示、布局辅助节点
- `HUDCanvas`
  承载模式按钮、右侧编辑面板、Hover 操作入口等 UI
- `Systems`
  放置所有全局控制器，避免逻辑散落在视图对象中

---

## 4. 模式状态机

## 4.1 模式定义
首版系统分为三种核心模式：

### Browse
大场景浏览模式。

职责：
- 浏览整体布局
- 观察宠物待机与轻交互反馈
- 选中单个趴趴
- 进入布局模式或单体编辑模式

### LayoutEdit
布局模式。

职责：
- 拖拽移动趴趴
- 吸附到合法位置
- 新增趴趴
- 删除趴趴
- 进入单体编辑模式

### SingleEdit
单体编辑模式。

职责：
- 聚焦单个趴趴
- 修改盒子相关配置
- 修改宠物相关配置
- 即时预览配置效果

## 4.2 模式流转

```text
Browse
  -> 点击编辑按钮 -> LayoutEdit
  -> 双击趴趴 -> SingleEdit

LayoutEdit
  -> 双击趴趴 -> SingleEdit
  -> 点击修改按钮 -> SingleEdit
  -> 点击完成 / Esc -> Browse

SingleEdit
  -> 点击返回 / Esc -> LayoutEdit
```

## 4.3 模式切换原则
- 所有模式切换统一由 `GameModeController` 管理
- 任何单体编辑退出后，只回到布局模式，不直接回到浏览模式
- `Esc` 的行为必须是“返回上一层模式”，不能跨层退出
- 模式切换时必须同步更新：
  - 相机状态
  - 输入路由
  - UI 可见性
  - 场景对象高亮/弱化状态

---

## 5. 核心运行时模块

## 5.1 GameModeController
全局模式控制器。

职责：
- 管理 `Browse / LayoutEdit / SingleEdit` 三态
- 校验是否允许切换
- 通知相机、UI、布局系统、编辑系统切换状态
- 记录当前选中趴趴与当前编辑趴趴

它是整个系统的顶层协调者，不直接处理具体拖拽或具体配置修改。

## 5.2 PaparLayoutController
布局控制器。

职责：
- 管理所有趴趴实例的空间关系
- 提供新增、删除、移动、吸附、合法性判定
- 管理布局模式下的新增提示与接缝提示
- 对外提供统一接口：
  - `CanPlaceAt`
  - `TryMove`
  - `CanDelete`
  - `CreateDefaultAt`

布局控制器不负责单体配置编辑。

## 5.3 PaparFactory
趴趴工厂。

职责：
- 创建默认趴趴实例
- 为新对象绑定默认配置
- 设置运行时 ID
- 初始化盒体视图和宠物视图

新增趴趴时统一由工厂创建，避免不同入口出现初始化不一致。

## 5.4 PaparInstance
单个趴趴的运行时对象。

职责：
- 保存当前配置
- 暴露可交互区域引用
- 响应选中、高亮、弱化、交互反馈
- 接收配置变化并刷新自身视图

它是布局系统和编辑系统共同操作的核心对象。

## 5.5 SingleEditController
单体编辑控制器。

职责：
- 绑定当前正在编辑的 `PaparInstance`
- 驱动右侧 `Box Tab / Pet Tab`
- 处理点击盒体切换 Box Tab，点击宠物切换 Pet Tab
- 将 UI 改动实时应用到当前实例

它只操作当前对象，不关心整体布局。

## 5.6 SelectionService
统一的选择与点击判定服务。

职责：
- Hover 检测
- 单击选中
- 双击进入编辑
- 点击空白取消选中
- UI 遮挡判断

该服务用于避免浏览模式和布局模式各自维护一套点击逻辑。

## 5.7 CameraRigController
相机控制器。

职责：
- Browse / LayoutEdit 共用平移与缩放
- SingleEdit 时执行 Zoom In 与目标聚焦
- 退出单体编辑后恢复到布局视角


---

## 6. 数据结构设计

## 6.1 运行时配置数据
首版每个趴趴实例至少应包含如下字段：

```text
PaparId
GridPosition / StackSlot
BoxShape
BoxStyleId
BoxColorId
FrameStyleId
PetTypeId
PetSkinId
PetFacing
```

## 6.2 字段说明
- `PaparId`
  每个实例的运行时唯一标识
- `GridPosition / StackSlot`
  当前在布局中的逻辑位置
- `BoxShape`
  盒体形状，例如长盒形、蛋筒形、甜甜圈形
- `BoxStyleId`
  盒体外壳样式
- `BoxColorId`
  盒体颜色
- `FrameStyleId`
  边框样式
- `PetTypeId`
  宠物种类
- `PetSkinId`
  宠物皮肤或颜色
- `PetFacing`
  宠物朝向，至少支持正面与背面

## 6.3 静态内容定义
建议使用 `ScriptableObject` 管理静态定义资源：
- `BoxShapeDefinition`
- `BoxStyleDefinition`
- `FrameStyleDefinition`
- `PetDefinition`
- `PaparPresetDefinition`

### 静态定义的职责
- 为 UI 提供可选项列表
- 为工厂提供默认创建数据
- 为视图刷新提供资源索引

首版可以只配置少量默认资源，但接口层面按可扩展方式设计。

---

## 7. 布局系统设计

## 7.1 目标
布局系统负责多个趴趴之间的空间关系。

## 7.2 合法连接方向
首版支持以下方向新增或吸附：
- 左侧
- 右侧
- 顶部

暂不支持自由旋转、任意角度摆放、复杂错位拼接。

## 7.3 移动流程
布局模式下拖拽一个趴趴时：
1. 进入拖拽状态
2. 显示预览占位
3. 检测最近合法连接位
4. 若合法，显示高亮预览
5. 若非法，显示禁用态预览
6. 松开后，若合法则放置；否则回到原位

## 7.4 新增流程
布局模式下 Hover 在接缝或边缘时：
1. 显示新增按钮
2. 点击后计算目标连接位
3. 调用 `PaparFactory` 创建默认趴趴
4. 加入 `LayoutRoot`
5. 刷新布局提示

新增对象默认使用基础盒型和默认宠物配置，不弹创建向导。

## 7.5 删除流程
删除前必须先检查：
- 当前对象上方是否有依附对象
- 当前对象是否为其他对象的承重点

首版规则：
- 若对象承担上层结构，则禁止删除
- 给出提示：“请先移走上方趴趴”
- 仅允许删除独立对象或无承重对象

## 7.6 Hover 操作提示
布局模式下 Hover 单个趴趴时：
- 高亮该趴趴
- 显示 `修改 / 删除`
- 在合法连接边显示 `+`


---

## 8. 单体编辑系统设计

## 8.1 编辑目标
单体编辑的目标是让玩家聚焦一个趴趴并快速修改其视觉组合。

首版不引入复杂参数编辑，只保留最核心的内容变更。

## 8.2 单体编辑入口
进入方式：
- 浏览模式下双击趴趴
- 布局模式下双击趴趴
- 布局模式下点击 Hover 操作中的“修改”

进入时：
- 相机拉近
- 当前对象高亮
- 其他对象弱化
- 打开右侧编辑面板

## 8.3 编辑面板结构
首版仅保留两个 Tab：

### Box Tab
负责修改盒子相关内容：
- 套装
- 外壳形状
- 外壳样式
- 颜色
- 边框

### Pet Tab
负责修改宠物相关内容：
- 种类
- 皮肤/颜色
- 朝向

## 8.4 点击切换逻辑
- 点击盒体或边框：切换到 `Box Tab`
- 点击宠物：切换到 `Pet Tab`

该规则是单体编辑模式的重要直觉入口，后续实现应优先保证。

## 8.5 即时预览原则
所有单体编辑项都应即时生效：
- 不设置额外确认按钮
- 修改后立即刷新视图
- 退出时保留当前修改结果

## 8.6 退出规则
- 点击返回按钮 -> 回到布局模式
- 按 `Esc` -> 回到布局模式

退出单体编辑时：
- 关闭右侧面板
- 恢复布局模式视角
- 保留刚才的改动

---

## 9. 输入与交互规则

## 9.1 Browse
- 右键拖拽：移动场景画布
- 滚轮：缩放视角
- Hover：高亮趴趴
- 单击：选中趴趴
- 双击：进入单体编辑
- 点击宠物：播放轻交互反馈，并视为选中
- 点击空白：取消选中

## 9.2 LayoutEdit
- 左键拖拽：移动趴趴
- Hover 边缘/接缝：显示新增按钮
- 点击 `+`：新增默认趴趴
- Hover 单个趴趴：显示修改/删除
- 双击或点击修改：进入单体编辑
- 点击完成或 `Esc`：返回浏览模式

## 9.3 SingleEdit
- 点击盒体：切到 Box Tab
- 点击宠物：切到 Pet Tab
- 点击右侧选项：即时应用
- 点击返回或 `Esc`：返回布局模式

## 9.4 UI 遮挡规则
- 指针停留在 UI 上时，不允许触发场景对象点击
- 拖拽布局时，若指针进入 UI 面板区域，应停止场景拖拽输入

---

## 10. UI 架构建议

## 10.1 顶层 UI
建议 UI 分为三类：

### HUD
常驻轻量 UI：
- 编辑按钮
- 返回/完成按钮
- 模式标题或提示

### Hover Actions
只在 Hover 或选中时出现：
- 修改
- 删除
- 新增按钮

### Single Edit Panel
仅在单体编辑模式显示：
- Box / Pet 切换
- 选项列表
- 返回按钮

## 10.2 UI 原则
- 浏览模式 UI 尽量轻
- 布局模式强调提示与结构
- 单体编辑模式强调面板与即时预览

---

## 11. 推荐脚本目录结构

```text
Assets/趴趴乐/Scripts
|- Core
|  |- GameModeController
|  |- CameraRigController
|  |- SelectionService
|- Layout
|  |- PaparLayoutController
|  |- LayoutSlot
|  |- LayoutHintView
|- Editing
|  |- SingleEditController
|  |- BoxEditPanel
|  |- PetEditPanel
|- Data
|  |- PaparConfig
|  |- BoxShapeDefinition
|  |- BoxStyleDefinition
|  |- FrameStyleDefinition
|  |- PetDefinition
|  |- PaparPresetDefinition
|- View
|  |- PaparInstance
|  |- PaparBoxView
|  |- PaparPetView
|- Factory
|  |- PaparFactory
```

这套结构的目的不是把代码拆得很细，而是保证职责清楚，便于后续继续扩展。

---

## 12. 首版不做项
为了保证首版尽快可玩，以下内容明确不在当前实现范围内：
- 本地存档 / 加载
- 撤销 / 重做
- 多选编辑
- 复杂自由形变
- 资源解锁与货币系统
- 盲盒系统
- 大量内容资产管理
- 高级动画状态机

这些内容可在核心交互跑通后再逐步加入。

---

## 13. 首版验收标准
首版完成后，至少应满足以下体验：

### 大场景浏览
- 能查看多个趴趴的整体布局
- 能平移和缩放视角
- 能选中、取消选中、双击进入编辑

### 布局模式
- 能拖动趴趴到合法位置
- 能在顶部、左侧、右侧新增趴趴
- 能删除非承重趴趴
- 能阻止删除承重趴趴

### 单体编辑模式
- 能切换 Box / Pet 两类内容
- 能即时预览盒型、颜色、边框、宠物种类与朝向
- 能从单体编辑返回布局模式

### 模式切换
- `Browse -> LayoutEdit -> SingleEdit -> LayoutEdit -> Browse` 链路完整稳定

---

## 14. 总结
《萌宠趴趴乐》首版程序系统的核心，不是复杂的资源或成长系统，而是三层稳定交互：
- 看整体
- 调布局
- 改单体

因此架构上最重要的不是提前做很多功能，而是先把以下三件事写稳：
- 模式状态机
- 布局与单体编辑的职责边界
- 数据驱动的即时预览

只要这三部分稳定，后续不管增加更多盒型、宠物、主题、收集系统还是存档系统，都可以在现有结构上继续扩展。
