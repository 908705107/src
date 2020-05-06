<template>
  <div>
    <!-- <a-button type="primary" @click="showDrawer">
      <a-icon type="plus" />New account
    </a-button>-->
    <a-drawer
      title="文章设置"
      :width="360"
      @close="onClose"
      :visible="visible"
      :bodyStyle="{paddingBottom: '80px'}"
      zIndex:99
    >
      <a-form :form="form" layout="vertical" hideRequiredMark>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="标题">
              <a-input v-decorator="['Title', {initialValue: this.myTitle}]" placeholder="无标题" />
            </a-form-item>
          </a-col>
          <a-divider />
          <!-- <a-col :span="12">
            <a-form-item label="Url">
              <a-input
                v-decorator="['url', {
                  rules: [{ required: true, message: 'please enter url' }]
                }]"
                style="width: 100%"
                addonBefore="http://"
                addonAfter=".com"
                placeholder="please enter url"
              />
            </a-form-item>
          </a-col>-->
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="作者">
              <a-input
                v-decorator="['owner', {
                  rules: [{ required: true, message: '请填写作者' }],initialValue: '陈林山'
                }]"
                placeholder="请填写作者"
              >
                <a-select-option value="xiao">Xiaoxiao Fu</a-select-option>
                <a-select-option value="mao">Maomao Zhou</a-select-option>
              </a-input>
            </a-form-item>
          </a-col>
          <a-col :span="6">
            <a-form-item label="评论">
              <a-switch
                defaultChecked
                v-decorator="['IsComment', {
                }]"
                checkedChildren="开"
                unCheckedChildren="关"
              />
            </a-form-item>
          </a-col>
          <a-col :span="6">
            <a-form-item label="置顶">
              <a-switch
                v-decorator="['IsToporQuality', {
                }]"
                checkedChildren="开"
                unCheckedChildren="关"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="分类">
              <a-tree-select
                v-decorator="[
                  'CategoryID',
                  {
                  },
                ]"
                showSearch
                style="width: 100%"
                :dropdownStyle="{ maxHeight: '400px', overflow: 'auto' }"
                placeholder="请选择文章分类"
                allowClear
                multiple
                treeDefaultExpandAll
                :treeData="AllTreeData"
              ></a-tree-select>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="标签">
              <a-select
                v-decorator="[
                  'TagID',
                  {
                    rules: [
                      { required: true, message: '请选择标签', type: 'array' },
                    ],
                  },
                ]"
                allowClear
                mode="tags"
                style="width: 100%"
                :tokenSeparators="[',']"
              >
                <a-select-option v-for="d in tagData" :key="d.Id">{{ d.TagName }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="导航图">
              <CUploadImg @func="GetFileList"></CUploadImg>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="描述">
              <a-textarea
                v-decorator="['Remark', {
                }]"
                :rows="4"
                placeholder="please enter url description"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
      <div
        :style="{
          position: 'absolute',
          right: 0,
          bottom: 0,
          width: '100%',
          borderTop: '1px solid #e9e9e9',
          padding: '10px 16px',
          background: '#fff',
          textAlign: 'right',
          zIndex: 1,
        }"
      >
        <a-button :style="{marginRight: '8px'}" @click="onClose">Cancel</a-button>
        <a-button @click="handleSubmit" type="primary">Submit</a-button>
      </div>
    </a-drawer>
  </div>
</template>
<script>
import CUploadImg from '@/components/CUploadImg/CUploadImg'
export default {
  props: {
    title: {
      type: String,
      default: '123123'
    }
  },
  watch: {
    title: function (newval, oldval) { this.myTitle = newval }
  },
  data () {
    return {
      form: this.$form.createForm(this),
      visible: false,
      myTitle: this.title,
      queryParam: { condition: 'type', keyword: '文章' },
      defaultValue: '',
      // config: {
      //   initialValue: this.myTitle
      // },
      AllTreeData: [],
      tagData: [],
      entity: {},
      fileList: []

    }
  },
  components: {
    CUploadImg
  },
  mounted () {
    this.init()
    // this.form.setFieldsValue({
    //   title: this.defaultValue
    // })
    // console.log(this.defaultValue)
  },
  methods: {

    GetFileList (list) {
      this.fileList = list
      console.log('接收到了子组件的filelist:' + list)
    },
    showDrawer () {
      this.visible = true
      this.myTitle = this.title
    },
    onClose () {
      this.visible = false
    },
    handleSubmit () {
      this.form.validateFields((errors, values) => {
        // 校验成功
        console.log(values)
        if (!errors) {
          this.entity = { fileList: this.fileList, ...Object.assign(this.entity, this.form.getFieldsValue()) }
          console.log(this.entity)
          this.$http.post('/ArticleManage/Article_Info/SaveData', this.entity).then(resJson => {
            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.visible = false
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    init () {
      // this.entity = {}
      // this.form.resetFields()
      this.$http.post('/DataManage/Data_Category/GetDataTreeList', { isall: true, ...this.queryParam }).then(resJson => {
        if (resJson.Success) {
          this.AllTreeData = resJson.Data// 所有的数据
          this.defaultValue = resJson.Data[0].value
        }
      })
      this.$http.post('/DataManage/Data_Tag/GetDataList').then(resJson => {
        if (resJson.Success) {
          this.tagData = resJson.Data// 所有的数据
        }
      })
    }

  }
}
</script>
