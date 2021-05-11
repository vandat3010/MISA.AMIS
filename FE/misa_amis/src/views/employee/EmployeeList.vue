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
                <th>#</th>
                <th>MÃ NHÂN VIÊN</th>
                <th>TÊN NHÂN VIÊN</th>
                <th>GIỚI TÍNH</th>
                <th>NGÀY SINH</th>
                <th>SỐ CMND</th>
                <th>CHỨC DANH</th>
                <th>TÊN ĐƠN VỊ</th>
                <th>SỐ TÀI KHOẢN</th>
                <th>TÊN NGÂN HÀNG</th>
                <th>CHI NHÁNH TK NGÂN HÀNG</th>
                <th>CHỨC NĂNG</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="e in employees" :key="e.employeeId">
                <td>
                  <input
                    type="checkbox"
                    class="checkbox"
                    :id="e.employeeCode"
                  />
                  <label :for="e.employeeCode"></label>
                </td>
                <td>{{ e.employeeCode }}</td>
                <td>{{ e.employeeName }}</td>
                <td>{{ e.genderName }}</td>
                <td>{{ e.dateOfBirth }}</td>
                <td>{{ e.identifyNumber }}</td>
                <td>{{ e.employeePosition }}</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                  <EmployeeDropdown
                    @onClickBtnEdit="onClickBtnEditEmployee(e.employeeId)"
                    @onClickBtnDel="onClickBtnDelEmployee(e)"
                  />
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="divider"></div>

        <EmployeePagination
          :pageIndex="pageIndex"
          :totalPages="totalPages"
          :totalRecord="totalRecord"
          @onChangePage="onChangePage"
        />
      </div>
    </div>
    <EmployeeDialog
      :isShow="isShowEmployeeDialog"
      :employee.sync="employeeModify"
      @onClose="setStateEmployeeDialog(false)"
      @onPositive="saveEmployee"
    />

    <AlertDialog
      :isShow="isShowAlertDialog"
      :employeeCode="employeeDel && employeeDel.employeeCode"
      @onPositive="delEmployee"
      @onClose="setStateAlertDialog(false)"
    />
  </div>
</template>

<script>
import req from '../../reponse/axios'

import StateEnum from "../../enum/StateEnum"

import EmployeePagination from "./EmployeePagination.vue";
import EmployeeDropdown from "./EmployeeDropdown.vue";
import EmployeeDialog from "./EmployeeDialog.vue";
import AlertDialog from "./AlertDialog.vue";

export default {
  components: {
    EmployeePagination,
    EmployeeDropdown,
    EmployeeDialog,
    AlertDialog,
  },
  data: () => ({
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
    pageSize: 20,

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
     * Thông tin nhân viên cần thêm hoặc sửa.
     *  createdBy: NVDAT(11/05/2021)
     */
    employeeModify: null,

    /**
     * Thông tin nhân viên cần xóa.
     *  createdBy: NVDAT(11/05/2021)
     */
    employeeDel: null,

    /**
     * Biến xác định trạng thái employee dialog.
     * CreatedBy: NVDAT (09/05/2021)
     */
    isShowEmployeeDialog: false,

    /**
     * Biến xác định trạng thái alert dialog.
     * CreatedBy: NVDAT (09/05/2021)
     */
    isShowAlertDialog: false,
    timeOut: null
  }),
  computed:{
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },
    isError: function () {
      return this.state == StateEnum.ERROR;
    },
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
          `api/v1/Employees?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}&filter=${this.filter}`
        )
        .then((res) => res.data)
        .then((data) => {
          this.employees = data.data;
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
      let val = e.target.value;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.filter = val;
        this.fetchEmployees();
      }, 1000);
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
      var reqConfig = {
        url: "api/v1/Employees",
        method: "POST",
        data: this.employeeModify,
      };
      if (this.employeeModify.employeeId) {
        // update
        reqConfig.method = "PUT";
      }

      req(reqConfig).then((res) => {
        if (res.status != 204) {
          this.setStateEmployeeDialog(false);
          this.fetchEmployees();
        }
      });
    },
    /**
     * Sự kiện thay đổi trang trong phân trang.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onChangePage(pageIndex) {
      this.pageIndex = pageIndex;
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
        .delete(`api/v1/Employees/${this.employeeDel.employeeId}`)
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

    showEmployeeDialog() {
      this.employeeModify = null;
      req("api/v1/Employees/NewEmployeeCode")
        .then((res) => res.data)
        .then((data) => {
          this.employeeModify = {
            employeeCode: data,
          };
          this.setStateEmployeeDialog(true);
        });
    },
     /**
     * Phương thức click button sửa nhân viên.
     * CreatedBy: NVDAT(11/05/2021)
     */
    onClickBtnEditEmployee(employeeId) {
      this.employeeModify = null;
      this.setStateEmployeeDialog(true);
      req(`api/v1/Employees/${employeeId}`)
        .then((res) => res.data)
        .then((data) => {
          this.employeeModify = data;
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
      this.isShowAlertDialog = state;
    },
  },
  mounted() {
    this.fetchEmployees();
  },
};
</script>