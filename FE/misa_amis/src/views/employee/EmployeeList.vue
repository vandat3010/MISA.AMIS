<template>
  <div>
    <div class="content">
      <div class="title-box">
        <div class="title">Nhân viên</div>
        <div class="toolbar">
          <button class="btn btn-primary" @click="showEmployeeDialog">
            Thêm mới nhân viên
          </button>
        </div>
      </div>

      <div class="toolbar-box">
        <div class="con-input">
          <input
            class="input has-icon"
            type="text"
            placeholder="Tìm theo mã, tên nhân viên"
            style="border-radius: 0"
            :value="filter"
            @input="onChangeInputEmployeeFilter"
          />
          <div class="icon-input icon icon-search"></div>
        </div>
        <div
          class="icon icon-refresh"
          style="margin-left: 8px"
          @click.prevent="onBtnClickRefresh"
        ></div>
        <div
          class="icon icon-excel"
          style="margin-left: 8px"
          @click.prevent="onBtnClickExportExcel"
        ></div>
      </div>

      <div class="data">
        <div class="scroll">
          <table class="table">
            <thead>
              <tr>
                <th>
                  <input
                    type="checkbox"
                    @click="checkAll()"
                    v-model="isCheckAll"
                  />
                </th>
                <th style="min-width: 150px">MÃ NHÂN VIÊN</th>
                <th style="min-width: 200px">TÊN NHÂN VIÊN</th>
                <th style="min-width: 100px">GIỚI TÍNH</th>
                <th style="min-width: 150px">NGÀY SINH</th>
                <th style="min-width: 150px">SỐ CMND</th>
                <th style="min-width: 150px">CHỨC DANH</th>
                <th style="min-width: 200px">TÊN ĐƠN VỊ</th>
                <th style="min-width: 150pxpx">SỐ TÀI KHOẢN</th>
                <th style="min-width: 100pxpx">TÊN NGÂN HÀNG</th>
                <th style="min-width: 100px">CHI NHÁNH TK NGÂN HÀNG</th>
                <!-- <th style="min-width: 83.33%">CHỨC NĂNG</th> -->
              </tr>
            </thead>
            <tbody>
              <tr v-for="(e, index) in employees" :key="index">
                <td>
                  <input
                    type="checkbox"
                    :id="e.employeeCode"
                    v-model="employeesTemp[e.employeeCode]"
                    @change="updateCheck($event, e.employeeCode)"
                  />
                  <label :for="e.employeeCode"></label>
                </td>
                <td>{{ e.employeeCode }}</td>
                <td>{{ e.fullName }}</td>
                <td>{{ e.genderName }}</td>
                <td>{{ e.dateOfBirth | formatDate }}</td>
                <td>{{ e.identifyNumber }}</td>
                <td>{{ e.positionName }}</td>
                <td>{{ e.departmentName }}</td>
                <td>{{ e.bankAccountNumber }}</td>
                <td>{{ e.bankName }}</td>
                <td>{{ e.bankBranchName }}</td>
                <!-- <td>
                  <EmployeeDropdown
                    @onClickBtnEdit="onClickBtnEditEmployee(e.employeeId)"
                    @onClickBtnDel="onClickBtnDelEmployee(e)"
                  />
                </td> -->
              </tr>
            </tbody>
          </table >
          <table class="table1" style="position: sticky; z-index=2; top:0; right: 0; boder:0;">
            <thead>
              <tr style="border-top: 0px solid #ccc;">
              <th style="min-width: 100px; postion: sticky; top:0;background-color: #f4f5f6">CHỨC NĂNG</th>
              </tr>
            </thead>
            <tbody style="background-color: white ">
                <tr v-for="e in employees" :key="e.employeeId"  style="background: white border: 1px solid #ccc;">
                  <td>
                  <EmployeeDropdown
                    @onClickBtnEdit="onClickBtnEditEmployee(e.employeeId)"
                    @onClickBtnDel="onClickBtnDelEmployee(e)"
                    @onClickBtnReplicate="onClickBtnReplicate(e.employeeId)"
                  />
                </td>
                </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="divider"></div>

      <EmployeePagination
        :pageIndex="pageIndex"
        :totalPages="totalPages"
        :totalRecord="totalRecord"
        @onChangePage="onChangePage"
        @onChangePageSize="onChangePageSize"
      />
    </div>

    <EmployeeDialog
      :isShow="isShowEmployeeDialog"
      :employee.sync="employeeModify"
      :departments="departments"
      @onClose="setStateEmployeeDialog(false)"
      @onSave="saveEmployee"
      @onSaveReset="saveResetEmployee"
    />

    <PopUpWarning
      :isShow="isShowConfirmDel"
      :message="`Bạn có thực sự muốn xóa nhân viên ${employeeDel.employeeCode} không?`"
      @onOk="delEmployee"
      @onClose="setStateAlertDialog(false)"
    />
    <PopUpWarning
      :isShow="requestStatus.isShowMessage"
      :message="requestStatus.message"
      :closeBtn="optionPopUpMessage.closeBtn"
      :okBtn="optionPopUpMessage.okBtn"
      @onClose="closeMesasge"
    />
  </div>
</template>

<script>
import req from "../../reponse/axios.js";

import StateEnum from "../../enum/StateEnum";

import EmployeePagination from "./EmployeePagination.vue";
import EmployeeDropdown from "./EmployeeDropdown.vue";
import EmployeeDialog from "./EmployeeDialog.vue";

import moment from "moment";

import PopUpWarning from "../popup/PopUpWarning";
const employeeDefault = {
  address: null,
  bankAccountNumber: null,
  bankBranchName: null,
  bankName: null,
  bankProvinceName: null,
  createdBy: null,
  createdDate: null,
  dateOfBirth: null,
  dateOfIN: null,
  departmentId: null,
  email: null,
  employeeCode: null,
  fullName: null,
  gender: 0,
  genderName: null,
  identifyNumber: null,
  modifiedBy: null,
  modifiedDate: null,
  phoneNumber: null,
  placeOfIN: null,
  positionName: null,
  telephoneNumber: null
};
export default {
  components: {
    EmployeePagination,
    EmployeeDropdown,
    EmployeeDialog,
    PopUpWarning,
  },
  data: () => ({
    /**
     * biến xác định check tất cả bản ghi hay không
     */
    isCheckAll: false,
    state: StateEnum.LOADING,
    /**
     * trang hien tai
     * createdBy: NVDAT(11/05/2021)
     */
    pageIndex: 1,

    /**
     * so ban ghi tren 1 trang
     * createdBy: NVDAT(11/05/2021)
     */
    pageSize: 10,

    /**
     * bộ lọc nhân viên
     * createdBy: NVDAT(11/05/2021)
     */

    filter: "",

    /**
     * tổng số trang
     * createdBy: NVDAT(11/05/2021)
     */
    totalPages: 0,
    /**
     * tổng số bản ghi
     * createdBy: NVDAT(11/05/2021)
     */
    totalRecord: 0,
    /**
     * Danh sách nhân viên
     *  createdBy: NVDAT(11/05/2021)
     */
    employees: [],

    /**
     * Danh sách đơn vị nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    departments: [],

    /**
     * Thông tin nhân viên cần thêm hoặc sửa.
     *  createdBy: NVDAT(11/05/2021)
     */
    employeeModify: employeeDefault,

    /**
     * Thông tin nhân viên cần xóa.
     *  createdBy: NVDAT(11/05/2021)
     */
    employeeDel: employeeDefault,

    /**
     * Biến xác định trạng thái employee dialog.
     * CreatedBy: NVDAT (09/05/2021)
     */
    isShowEmployeeDialog: false,

    /**
     * Biến xác định trạng thái alert dialog.
     * CreatedBy: NVDAT (09/05/2021)
     */
    isShowConfirmDel: false,

    timeOut: null,
    /**
     * biếm lưu tạm của hàm checkALL
     * createdBy: NVDAT(11/05/2021)
     */
    employeesTemp: [],
    /**
     *cảnh báo khi nhập sai
     *CreatedBy: NVDAT(11/05/2021)
     */
    requestStatus: {
      isShowMessage: false,
      message: "",
    },
    /**
     * biến gán cho nhân bản data
     * createdBy: NVDAT(17/05/2021)
     */
    employeeReplicate: employeeDefault,
    
    /**
     *các button trên popUp
     *CreatedBy: NVDAT(15/05/2021)
     */
    optionPopUpMessage: {
      closeBtn: { isShow: true, label: "Đóng" },
      okBtn: { isShow: false },
    },
  }),
  filters: {
    formatDate: function (date) {
      if (date) {
        return moment(String(date)).format("DD/MM/YYYY");
      }
    },
  },
  computed: {
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },
    isError: function () {
      return this.state == StateEnum.ERROR;
    },
  },
  created() {
    this.fetchEmployees();
    this.fetchDepartment();
  },
  mounted() {
    this.fetchEmployees();
  },
  methods: {
    /**
     * Lấy dữ liệu từ api.
     * CreatedBy: NVDAT(11/05/2021)
     */
    fetchEmployees() {
      this.state = StateEnum.LOADING;
      req
        .get(
          `api/v1/Employees/Filter?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}&filter=${this.filter}`
        )
        .then((res) => {
          const data = res.data;
          this.employees = data.data.map((item) => {
            item.departmentName = this.getDepartmentName(item.departmentId);
            return item;
          });
          this.totalPages = data.totalPages;
          this.totalRecord = data.totalRecord;
          this.state = StateEnum.SUCCESS;
        })
        .catch(() => {
          this.state = StateEnum.ERROR;
        });
    },
    /**
     * Phương thức được gọi khi thay đổi input filter nhân viên.
     * @param {Element} e
     * CreatedBy: NVDAT(11/05/2021)
     */
    onChangeInputEmployeeFilter(e) {
      let value = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.filter = value;
        this.pageIndex = 1;
        this.fetchEmployees();
      }, 1500);
    },
    /**
     * Phương thức click button refresh.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onBtnClickRefresh() {
      this.filter = "";
      this.pageIndex = 1;
      this.fetchEmployees();
    },
    /**
     * click button xuất excel.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onBtnClickExportExcel() {
      window.open(
        `https://localhost:44341/api/v1/Employees/Export?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}&filter=${this.filter}`,
        "_blank"
      );
    },

    /**
     * Lưu thông tin nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    saveEmployee() {
      this.state = StateEnum.LOADING;
      this.employeeCodeExists = null;
      var reqConfig = {
        url: "api/v1/Employees",
        method: "POST",
        data: this.employeeModify,
      };
      if (this.employeeModify.employeeId) {
        // update
        reqConfig.method = "PUT";
        reqConfig.url = `api/v1/Employees/${this.employeeModify.employeeId}`;
      }
      var _self = this;
      req(reqConfig)
        .then((res) => {
          if (res.status != 204) {
            this.setStateEmployeeDialog(false);
            this.fetchEmployees();
          }
        })
        .catch(function (error) {
          _self.requestStatus.isShowMessage = true;
          if (error.response && error.response.data.devMsg) {
            _self.requestStatus.message = error.response.data.devMsg;
          } else {
            _self.requestStatus.message = "Có lỗi xảy ra vui lòng liên hệ MISA!";
          }
        })
        .finally(() => {
          this.state = StateEnum.SUCCESS;
        });
    },
    /**
     * lưu thông tin nhân viên và reset lại from
     * CreatedBy: NVDAT(16/05/2020)
     */
    saveResetEmployee() {
      this.state = StateEnum.LOADING;
      this.employeeCodeExists = null;
      var reqConfig = {
        url: "api/v1/Employees",
        method: "POST",
        data: this.employeeModify,
      };
      if (this.employeeModify.employeeId) {
        // update
        reqConfig.method = "PUT";
        reqConfig.url = `api/v1/Employees/${this.employeeModify.employeeId}`;
      }
      var _self = this;
      req(reqConfig)
        .then((res) => {
          if (res.status != 204) {
            this.showEmployeeDialog();
            this.fetchEmployees();
            this.employeeModify = employeeDefault;
          }
        })
        .catch(function (error) {
          _self.requestStatus.isShowMessage = true;
          if (error.response && error.response.data.devMsg) {
            _self.requestStatus.message = error.response.data.devMsg;
          }
        })
        .finally(() => {
          this.state = StateEnum.SUCCESS;
        });
    },
    /**
     * Sự kiện thay đổi trang trong phân trang.
     * CreatedBy: NVDAT (10/05/2021)
     */
    onChangePage(pageIndex) {
      this.pageIndex = pageIndex;
      this.fetchEmployees();
    },

    /**
     * Sự kiện thay đổi số bản ghi trên trang.
     * CreatedBy: NVDAT (10/05/2021)
     */
    onChangePageSize(pageSize) {
      this.pageSize = pageSize;
      this.fetchEmployees();
    },
    /**
     * Phương thức xóa nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    delEmployee() {
      this.state = StateEnum.LOADING;
      this.setStateAlertDialog(false);
      req
        .delete(`api/v1/Employees?entityId=${this.employeeDel.employeeId}`)
        .then((res) => {
          if (res.status == 200) {
            this.fetchEmployees();
          }
        });
    },
    /**
     * Phương thức set trạng thái employee dialog
     * @param {Boolean} state
     * CreatedBy: NVDAT(11/05/2021)
     */
    setStateEmployeeDialog(state) {
      this.isShowEmployeeDialog = state;
    },
    showEmployeeDialog(isClearData = true) {
      if(isClearData){
        this.employeeModify = employeeDefault;
      }
      req("api/v1/Employees/NewEmployeeCode")
        .then((res) => res.data)
        .then((data) => {
          this.employeeModify.employeeCode = data;
          this.setStateEmployeeDialog(true);
        });
      this.fetchDepartment();
    },
    /**
     * Phương thức click button sửa nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onClickBtnEditEmployee(employeeId) {
      this.employeeModify = employeeDefault;
      this.setStateEmployeeDialog(true);
      req(`api/v1/Employees/${employeeId}`)
        .then((res) => res.data)
        .then((data) => {
          this.employeeModify = data;
        });
      this.fetchDepartment();
    },
    /**
     * Phương thức click button nhân bản nhân viên
     * CreatedBy: NVDAT(17/05/2021)
     */
    onClickBtnReplicate(employeeId){
      req(`api/v1/Employees/${employeeId}`)
        .then((res) => {
          this.employeeModify = res.data;
          delete this.employeeModify.employeeId;
          this.showEmployeeDialog(false);
        });
        
    },

    /**
     * Phương thức click button xóa nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onClickBtnDelEmployee(employee) {
      this.employeeDel = employee;
      this.setStateAlertDialog(true);
    },

    /**
     * Phương thức set trạng thái employee dialog
     * @param {Boolean} state
     * CreatedBy: NVDAT (09/05/2021)
     */
    setStateAlertDialog(state) {
      this.isShowConfirmDel = state;
    },
    /**
     * Lấy dữ liệu bộ phận từ api.
     * CreatedBy: NVDAT(10/05/2021)
     */
    fetchDepartment() {
      req("api/v1/Departments")
        .then((res) => res.data)
        .then((data) => {
          this.departments = data;
        });
    },
    /**
     * lấy ra tên phòng ban bộ phận
     * CreatedBy: NVDAT(11/05/2021)
     */
    getDepartmentName(departmentId) {
      if (!departmentId || !this.departments || this.departments.length <= 0) {
        return "";
      }
      const depm = this.departments.find(
        (p) => p.departmentId === departmentId
      );
      if (depm) {
        return depm.departmentName;
      }
      return "";
    },

    /**
     * chọn tất cả
     * CreatedBy(11/05/2021)
     */
    checkAll() {
      this.isCheckAll = !this.isCheckAll;
      this.employeesTemp = [];
      if (this.isCheckAll) {
        // Check all
        this.employees.forEach((item) => {
          this.employeesTemp[item.employeeCode] = true;
        });
      }
    },

    /**
     * chọn hết các bản ghi tren cùng 1 trang thì sẽ hiện chọn tất cả
     * CreatedBy(11/05/2021)
     */
    updateCheck(event, employeeCode) {
      if (!event.target.checked) {
        delete this.employeesTemp[employeeCode];
      } else {
        this.employeesTemp[employeeCode] = true;
      }
      if (this.employees.length == Object.keys(this.employeesTemp).length) {
        this.isCheckAll = true;
      } else {
        this.isCheckAll = false;
      }
    },
    closeMesasge() {
      this.requestStatus.isShowMessage = false;
    },
  },
};
</script>