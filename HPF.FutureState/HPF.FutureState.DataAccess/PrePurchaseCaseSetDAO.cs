using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common;
using System.Data;

namespace HPF.FutureState.DataAccess
{
    public class PrePurchaseCaseSetDAO : BaseDAO
    {
        public SqlConnection dbConnection;
        public SqlTransaction trans;

        protected PrePurchaseCaseSetDAO() { }
        public static PrePurchaseCaseSetDAO CreateInstance()
        {
            return new PrePurchaseCaseSetDAO();
        }
        #region Manage Transaction
        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary>
        /// Commit Working
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        /// <summary>
        /// Rollback working
        /// </summary>
        public void Rollback()
        {
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        public void CloseConnection()
        {
            try
            {
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
        }
        #endregion
        /// <summary>
        /// Insert PrePurchase Case
        /// </summary>
        /// <param name="prePurchaseCase">PrePurchaseCaseDTO</param>
        /// <returns></returns>
        public int? InsertPrePurchaseCase(PrePurchaseCaseDTO prePurchaseCase)
        {
            var command = CreateSPCommand("hpf_pre_purchase_case_insert", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[45];

                sqlParam[0] = new SqlParameter("@pi_applicant_id", prePurchaseCase.ApplicantId);
                sqlParam[1] = new SqlParameter("@pi_agency_id", prePurchaseCase.AgencyId);
                sqlParam[2] = new SqlParameter("@pi_agency_case_num", NullableString(prePurchaseCase.AgencyCaseNum));
                sqlParam[3] = new SqlParameter("@pi_acct_num", NullableString(prePurchaseCase.AcctNum));
                sqlParam[4] = new SqlParameter("@pi_mortgage_program_cd", NullableString(prePurchaseCase.MortgageProgramCd));
                sqlParam[5] = new SqlParameter("@pi_borrower_fname", NullableString(prePurchaseCase.BorrowerFName));
                sqlParam[6] = new SqlParameter("@pi_borrower_lname", NullableString(prePurchaseCase.BorrowerLName));
                sqlParam[7] = new SqlParameter("@pi_co_borrower_fname", NullableString(prePurchaseCase.CoBorrowerFName));
                sqlParam[8] = new SqlParameter("@pi_co_borrower_lname", NullableString(prePurchaseCase.CoBorrowerLName));
                sqlParam[9] = new SqlParameter("@pi_prop_addr1", NullableString(prePurchaseCase.PropAddr1));
                sqlParam[10] = new SqlParameter("@pi_prop_addr2", NullableString(prePurchaseCase.PropAddr2));
                sqlParam[11] = new SqlParameter("@pi_prop_city", NullableString(prePurchaseCase.PropCity));
                sqlParam[12] = new SqlParameter("@pi_prop_state_cd", NullableString(prePurchaseCase.PropStateCd));
                sqlParam[13] = new SqlParameter("@pi_prop_zip", NullableString(prePurchaseCase.PropZip));
                sqlParam[14] = new SqlParameter("@pi_mail_addr1", NullableString(prePurchaseCase.MailAddr1));
                sqlParam[15] = new SqlParameter("@pi_mail_addr2", NullableString(prePurchaseCase.MailAddr2));
                sqlParam[16] = new SqlParameter("@pi_mail_city", NullableString(prePurchaseCase.MailCity));
                sqlParam[17] = new SqlParameter("@pi_mail_state_cd", NullableString(prePurchaseCase.MailStateCd));
                sqlParam[18] = new SqlParameter("@pi_mail_zip", NullableString(prePurchaseCase.MailZip));
                sqlParam[19] = new SqlParameter("@pi_borrower_authorization_ind", NullableString(prePurchaseCase.BorrowerAuthorizationInd));
                sqlParam[20] = new SqlParameter("@pi_mother_maiden_lname", NullableString(prePurchaseCase.MotherMaidenLName));
                sqlParam[21] = new SqlParameter("@pi_primary_contact_no", NullableString(prePurchaseCase.PrimaryContactNo));
                sqlParam[22] = new SqlParameter("@pi_secondary_contact_no", NullableString(prePurchaseCase.SecondaryContactNo));
                sqlParam[23] = new SqlParameter("@pi_borrower_employer_name", NullableString(prePurchaseCase.BorrowerEmployerName));
                sqlParam[24] = new SqlParameter("@pi_borrower_job_title", NullableString(prePurchaseCase.BorrowerJobTitle));
                sqlParam[25] = new SqlParameter("@pi_borrower_self_employed_ind", NullableString(prePurchaseCase.BorrowerSelfEmployedInd));
                sqlParam[26] = new SqlParameter("@pi_borrower_years_employed", prePurchaseCase.BorrowerYearsEmployed);
                sqlParam[27] = new SqlParameter("@pi_co_borrower_employer_name", NullableString(prePurchaseCase.CoBorrowerEmployerName));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_job_title", NullableString(prePurchaseCase.CoBorrowerJobTitle));
                sqlParam[29] = new SqlParameter("@pi_co_borrower_self_employed_ind", NullableString(prePurchaseCase.CoBorrowerSelfEmployedInd));
                sqlParam[30] = new SqlParameter("@pi_co_borrower_years_employed", prePurchaseCase.CoBorrowerYearsEmployed);
                sqlParam[31] = new SqlParameter("@pi_counselor_id_ref", NullableString(prePurchaseCase.CounselorIdRef));
                sqlParam[32] = new SqlParameter("@pi_counselor_fname", NullableString(prePurchaseCase.CounselorFName));
                sqlParam[33] = new SqlParameter("@pi_counselor_lname", NullableString(prePurchaseCase.CounselorLName));
                sqlParam[34] = new SqlParameter("@pi_counselor_email", NullableString(prePurchaseCase.CounselorEmail));
                sqlParam[35] = new SqlParameter("@pi_counselor_phone", NullableString(prePurchaseCase.CounselorPhone));
                sqlParam[36] = new SqlParameter("@pi_counselor_ext", NullableString(prePurchaseCase.CounselorExt));
                sqlParam[37] = new SqlParameter("@pi_counseling_duration_mins", prePurchaseCase.CounselingDurationMins);
                sqlParam[38] = new SqlParameter("@pi_create_dt",prePurchaseCase.CreateDate);
                sqlParam[39] = new SqlParameter("@pi_create_user_id",prePurchaseCase.CreateUserId);
                sqlParam[40] = new SqlParameter("@pi_create_app_name",prePurchaseCase.CreateAppName);
                sqlParam[41] = new SqlParameter("@pi_chg_lst_dt",prePurchaseCase.ChangeLastDate);
                sqlParam[42] = new SqlParameter("@pi_chg_lst_user_id",prePurchaseCase.ChangeLastUserId);
                sqlParam[43] = new SqlParameter("@pi_chg_lst_app_name",prePurchaseCase.ChangeLastAppName);

                sqlParam[44] = new SqlParameter("@po_ppc_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                prePurchaseCase.PpcId = ConvertToInt(sqlParam[44].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
            return prePurchaseCase.PpcId;
        }
        /// <summary>
        /// Update PrePurchase Case
        /// </summary>
        /// <param name="prePurchaseCase">PrePurchaseCaseDTO</param>
        public int? UpdatePrePurchaseCase(PrePurchaseCaseDTO prePurchaseCase)
        {
            var command = CreateSPCommand("hpf_pre_purchase_case_update", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[42];

                sqlParam[0] = new SqlParameter("@pi_applicant_id", prePurchaseCase.ApplicantId);
                sqlParam[1] = new SqlParameter("@pi_agency_id", prePurchaseCase.AgencyId);
                sqlParam[2] = new SqlParameter("@pi_agency_case_num", NullableString(prePurchaseCase.AgencyCaseNum));
                sqlParam[3] = new SqlParameter("@pi_acct_num", NullableString(prePurchaseCase.AcctNum));
                sqlParam[4] = new SqlParameter("@pi_mortgage_program_cd",NullableString(prePurchaseCase.MortgageProgramCd));
                sqlParam[5] = new SqlParameter("@pi_borrower_fname",NullableString(prePurchaseCase.BorrowerFName));
                sqlParam[6] = new SqlParameter("@pi_borrower_lname",NullableString(prePurchaseCase.BorrowerLName));
                sqlParam[7] = new SqlParameter("@pi_co_borrower_fname",NullableString(prePurchaseCase.CoBorrowerFName));
                sqlParam[8] = new SqlParameter("@pi_co_borrower_lname",NullableString(prePurchaseCase.CoBorrowerLName));
                sqlParam[9] = new SqlParameter("@pi_prop_addr1",NullableString(prePurchaseCase.PropAddr1));
                sqlParam[10] = new SqlParameter("@pi_prop_addr2",NullableString(prePurchaseCase.PropAddr2));
                sqlParam[11] = new SqlParameter("@pi_prop_city",NullableString(prePurchaseCase.PropCity));
                sqlParam[12] = new SqlParameter("@pi_prop_state_cd",NullableString(prePurchaseCase.PropStateCd));
                sqlParam[13] = new SqlParameter("@pi_prop_zip",NullableString(prePurchaseCase.PropZip));
                sqlParam[14] = new SqlParameter("@pi_mail_addr1",NullableString(prePurchaseCase.MailAddr1));
                sqlParam[15] = new SqlParameter("@pi_mail_addr2",NullableString(prePurchaseCase.MailAddr2));
                sqlParam[16] = new SqlParameter("@pi_mail_city",NullableString(prePurchaseCase.MailCity));
                sqlParam[17] = new SqlParameter("@pi_mail_state_cd",NullableString(prePurchaseCase.MailStateCd));
                sqlParam[18] = new SqlParameter("@pi_mail_zip",NullableString(prePurchaseCase.MailZip));
                sqlParam[19] = new SqlParameter("@pi_borrower_authorization_ind", NullableString(prePurchaseCase.BorrowerAuthorizationInd));
                sqlParam[20] = new SqlParameter("@pi_mother_maiden_lname", NullableString(prePurchaseCase.MotherMaidenLName));
                sqlParam[21] = new SqlParameter("@pi_primary_contact_no", NullableString(prePurchaseCase.PrimaryContactNo));
                sqlParam[22] = new SqlParameter("@pi_secondary_contact_no", NullableString(prePurchaseCase.SecondaryContactNo));
                sqlParam[23] = new SqlParameter("@pi_borrower_employer_name", NullableString(prePurchaseCase.BorrowerEmployerName));
                sqlParam[24] = new SqlParameter("@pi_borrower_job_title", NullableString(prePurchaseCase.BorrowerJobTitle));
                sqlParam[25] = new SqlParameter("@pi_borrower_self_employed_ind", NullableString(prePurchaseCase.BorrowerSelfEmployedInd));
                sqlParam[26] = new SqlParameter("@pi_borrower_years_employed", prePurchaseCase.BorrowerYearsEmployed);
                sqlParam[27] = new SqlParameter("@pi_co_borrower_employer_name", NullableString(prePurchaseCase.CoBorrowerEmployerName));
                sqlParam[28] = new SqlParameter("@pi_co_borrower_job_title", NullableString(prePurchaseCase.CoBorrowerJobTitle));
                sqlParam[29] = new SqlParameter("@pi_co_borrower_self_employed_ind", NullableString(prePurchaseCase.CoBorrowerSelfEmployedInd));
                sqlParam[30] = new SqlParameter("@pi_co_borrower_years_employed", prePurchaseCase.CoBorrowerYearsEmployed);
                sqlParam[31] = new SqlParameter("@pi_counselor_id_ref", NullableString(prePurchaseCase.CounselorIdRef));
                sqlParam[32] = new SqlParameter("@pi_counselor_fname", NullableString(prePurchaseCase.CounselorFName));
                sqlParam[33] = new SqlParameter("@pi_counselor_lname", NullableString(prePurchaseCase.CounselorLName));
                sqlParam[34] = new SqlParameter("@pi_counselor_email", NullableString(prePurchaseCase.CounselorEmail));
                sqlParam[35] = new SqlParameter("@pi_counselor_phone", NullableString(prePurchaseCase.CounselorPhone));
                sqlParam[36] = new SqlParameter("@pi_counselor_ext", NullableString(prePurchaseCase.CounselorExt));
                sqlParam[37] = new SqlParameter("@pi_counseling_duration_mins", prePurchaseCase.CounselingDurationMins);
                
                sqlParam[38] = new SqlParameter("@pi_chg_lst_dt", prePurchaseCase.ChangeLastDate);
                sqlParam[39] = new SqlParameter("@pi_chg_lst_user_id", prePurchaseCase.ChangeLastUserId);
                sqlParam[40] = new SqlParameter("@pi_chg_lst_app_name", prePurchaseCase.ChangeLastAppName);

                sqlParam[41] = new SqlParameter("@pi_ppc_id",prePurchaseCase.PpcId );
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
            return prePurchaseCase.PpcId;
        }

        /// <summary>
        /// Insert a PPBudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">PPBudgetSetDTO</param>
        /// <returns></returns>
        public int? InsertPPBudgetSet(PPBudgetSetDTO ppBudgetSet, int? ppc_id)
        {
            var command = new SqlCommand("hpf_pp_budget_set_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[12];
                sqlParam[0] = new SqlParameter("@pi_ppc_id", ppc_id);
                sqlParam[1] = new SqlParameter("@pi_total_income", ppBudgetSet.TotalIncome);
                sqlParam[2] = new SqlParameter("@pi_total_expenses", ppBudgetSet.TotalExpenses);
                sqlParam[3] = new SqlParameter("@pi_total_assets", ppBudgetSet.TotalAssets);
                sqlParam[4] = new SqlParameter("@pi_pp_budget_set_dt", NullableDateTime(ppBudgetSet.PPBudgetSetDt));
                sqlParam[5] = new SqlParameter("@pi_create_dt", NullableDateTime(ppBudgetSet.CreateDate));
                sqlParam[6] = new SqlParameter("@pi_create_user_id", ppBudgetSet.CreateUserId);
                sqlParam[7] = new SqlParameter("@pi_create_app_name", ppBudgetSet.CreateAppName);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(ppBudgetSet.ChangeLastDate));
                sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", ppBudgetSet.ChangeLastUserId);
                sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", ppBudgetSet.ChangeLastAppName);
                sqlParam[11] = new SqlParameter("@po_pp_budget_set_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                ppBudgetSet.PPBudgetSetId = ConvertToInt(sqlParam[11].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            return ppBudgetSet.PPBudgetSetId;
        }

        /// <summary>
        /// Insert a Proposed Pre-Purchase BudgetSet to database.
        /// </summary>
        /// <param name="budgetSet">PPPBudgetSetDTO</param>
        /// <returns></returns>
        public int? InsertProposedPPBudgetSet(PPPBudgetSetDTO pppBudgetSet, int? ppc_id)
        {
            var command = new SqlCommand("hpf_ppp_budget_set_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[12];
                sqlParam[0] = new SqlParameter("@pi_ppc_id", ppc_id);
                sqlParam[1] = new SqlParameter("@pi_total_income", pppBudgetSet.TotalIncome);
                sqlParam[2] = new SqlParameter("@pi_total_expenses", pppBudgetSet.TotalExpenses);
                sqlParam[3] = new SqlParameter("@pi_total_assets", pppBudgetSet.TotalAssets);
                sqlParam[4] = new SqlParameter("@pi_ppp_budget_set_dt", NullableDateTime(pppBudgetSet.PPPBudgetSetDt));
                sqlParam[5] = new SqlParameter("@pi_create_dt", NullableDateTime(pppBudgetSet.CreateDate));
                sqlParam[6] = new SqlParameter("@pi_create_user_id", pppBudgetSet.CreateUserId);
                sqlParam[7] = new SqlParameter("@pi_create_app_name", pppBudgetSet.CreateAppName);
                sqlParam[8] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(pppBudgetSet.ChangeLastDate));
                sqlParam[9] = new SqlParameter("@pi_chg_lst_user_id", pppBudgetSet.ChangeLastUserId);
                sqlParam[10] = new SqlParameter("@pi_chg_lst_app_name", pppBudgetSet.ChangeLastAppName);
                sqlParam[11] = new SqlParameter("@po_ppp_budget_set_id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
                pppBudgetSet.PPPBudgetSetId = ConvertToInt(sqlParam[11].Value);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
            return pppBudgetSet.PPPBudgetSetId;
        }

        /// <summary>
        /// Insert a Proposed BudgetItem to database.
        /// </summary>
        /// <param name="budgetItem">PPBudgetItemDTO</param>
        /// <returns></returns>
        public void InsertPPBudgetItem(PPBudgetItemDTO ppBudgetItem, int? ppBudgetSetId)
        {
            var command = CreateCommand("hpf_pp_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("pi_pp_budget_set_id", ppBudgetSetId);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", ppBudgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_pp_budget_item_amt", ppBudgetItem.PPBudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_pp_budget_note", NullableString(ppBudgetItem.PPBudgetNote));
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(ppBudgetItem.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", ppBudgetItem.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", ppBudgetItem.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(ppBudgetItem.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", ppBudgetItem.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", ppBudgetItem.ChangeLastAppName);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
        }

        /// <summary>
        /// Insert a Proposed Pre-Purchase BudgetItem to database.
        /// </summary>
        /// <param name="budgetItem">PPPBudgetItemDTO</param>
        /// <returns></returns>
        public void InsertProposedPPBudgetItem(PPPBudgetItemDTO pppBudgetItem, int? pppBudgetSetId)
        {
            var command = CreateCommand("hpf_ppp_budget_item_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_ppp_budget_set_id", pppBudgetSetId);
                sqlParam[1] = new SqlParameter("@pi_budget_subcategory_id", pppBudgetItem.BudgetSubcategoryId);
                sqlParam[2] = new SqlParameter("@pi_proposed_budget_item_amt", pppBudgetItem.ProposedBudgetItemAmt);
                sqlParam[3] = new SqlParameter("@pi_proposed_budget_note", NullableString(pppBudgetItem.ProposedBudgetNote));
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(pppBudgetItem.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", pppBudgetItem.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", pppBudgetItem.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(pppBudgetItem.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", pppBudgetItem.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", pppBudgetItem.ChangeLastAppName);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
        }

        /// <summary>
        /// Update Applicant
        /// </summary>
        /// <param name="prePurchaseCase">ApplicantDTO</param>
        public void UpdateApplicant(ApplicantDTO applicant)
        {
            var command = CreateSPCommand("hpf_applicant_update", this.dbConnection);
            try
            {
                var sqlParam = new SqlParameter[17];

                sqlParam[0] = new SqlParameter("@pi_applicant_id", applicant.ApplicantId);
                sqlParam[1] = new SqlParameter("@right_party_contact_ind",NullableString(applicant.RightPartyContactInd));
                sqlParam[2] = new SqlParameter("@rpc_most_recent_dt", applicant.RpcMostRecentDt);
                sqlParam[3] = new SqlParameter("@no_rpc_reason", NullableString(applicant.NoRpcReason));
                sqlParam[4] = new SqlParameter("@counseling_accepted_dt", applicant.CounselingAcceptedDt);
                sqlParam[5] = new SqlParameter("@counseling_scheduled_dt", applicant.CounselingScheduledDt);
                sqlParam[6] = new SqlParameter("@counseling_completed_dt", applicant.CounselingCompletedDt);
                sqlParam[7] = new SqlParameter("@counseling_refused_dt", applicant.CounselingRefusedDt);
                sqlParam[8] = new SqlParameter("@first_counseled_dt", applicant.FirstCounseledDt);
                sqlParam[9] = new SqlParameter("@second_counseled_dt", applicant.SecondCounseledDt);
                sqlParam[10] = new SqlParameter("@ed_module_completed_dt", applicant.EdModuleCompletedDt);
                sqlParam[11] = new SqlParameter("@inbound_call_to_num_dt", applicant.InboundCallToNumDt);
                sqlParam[12] = new SqlParameter("@inbound_call_to_num_reason", NullableString(applicant.InboundCallToNumReason));
                sqlParam[13] = new SqlParameter("@actual_close_dt", applicant.ActualCloseDt);

                sqlParam[14] = new SqlParameter("@pi_chg_lst_dt", applicant.ChangeLastDate);
                sqlParam[15] = new SqlParameter("@pi_chg_lst_user_id", applicant.ChangeLastUserId);
                sqlParam[16] = new SqlParameter("@pi_chg_lst_app_name", applicant.ChangeLastAppName);
                //</Parameter> 
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;
                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
        }

        /// <summary>
        /// Insert a Pre-Purchase BudgetAsset to database.
        /// </summary>
        /// <param name="budgetItem">PPBudgetAssetDTO</param>
        /// <returns></returns>
        public void InsertPPBudgetAsset(PPBudgetAssetDTO ppBudgetAsset, int? ppBudgetSetId)
        {
            var command = CreateCommand("hpf_pp_budget_asset_insert", this.dbConnection);
            //<Parameter>
            try
            {
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_pp_budget_set_id", ppBudgetSetId);
                sqlParam[1] = new SqlParameter("@pi_pp_budget_asset_name", NullableString(ppBudgetAsset.PPBudgetAssetName));
                sqlParam[2] = new SqlParameter("@pi_pp_budget_asset_value", ppBudgetAsset.PPBudgetAssetValue);
                sqlParam[3] = new SqlParameter("@pi_pp_budget_asset_note", NullableString(ppBudgetAsset.PPBudgetAssetNote));
                sqlParam[4] = new SqlParameter("@pi_create_dt", NullableDateTime(ppBudgetAsset.CreateDate));
                sqlParam[5] = new SqlParameter("@pi_create_user_id", ppBudgetAsset.CreateUserId);
                sqlParam[6] = new SqlParameter("@pi_create_app_name", ppBudgetAsset.CreateAppName);
                sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", NullableDateTime(ppBudgetAsset.ChangeLastDate));
                sqlParam[8] = new SqlParameter("@pi_chg_lst_user_id", ppBudgetAsset.ChangeLastUserId);
                sqlParam[9] = new SqlParameter("@pi_chg_lst_app_name", ppBudgetAsset.ChangeLastAppName);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.trans;

                command.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
            }
        }
        public ApplicantDTO GetExistingApplicantId(int? applicant_id)
        {
            ApplicantDTO returnObject = null;
            var dbConnection = CreateConnection();
            SqlCommand command = base.CreateCommand("hpf_applicant_get_by_id", dbConnection);
            try
            {
                //<Parameter>
                var sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_applicant_id", applicant_id);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        returnObject = new ApplicantDTO();
                        returnObject.ApplicantId = ConvertToInt(reader["applicant_id"]);
                        returnObject.SentToAgencyId = ConvertToInt(reader["sent_to_agency_id"]);
                    }
                }
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
            return returnObject;
        }

        public bool CheckExistingAgencyIdAndCaseNumber(int? agency_id, string agency_case_number)
        {
            bool returnValue = true;
            var dbConnection = CreateConnection();
            SqlCommand command = base.CreateCommand("hpf_pre_purchase_case_get_from_agencyID_and_caseNumber", dbConnection);//new SqlCommand("hpf_foreclosure_case_get_from_agencyID_and_caseNumber", dbConnection);
            try
            {
                //<Parameter>
                var sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@pi_agency_case_num", agency_case_number);
                sqlParam[1] = new SqlParameter("@pi_agency_id", agency_id);
                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                returnValue = reader.HasRows;
                reader.Close();
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
            return returnValue;

        }  

        /// <summary>
        /// get Pre-Purchase Case
        /// </summary>
        /// <param name="PpcId">id of a Pre-Purchase Case</param>
        /// <returns>PrePurchaseCase if exists, otherwise: null</returns>
        public PrePurchaseCaseDTO GetPrePurchaseCase(int? applicantId)
        {
            PrePurchaseCaseDTO returnObject = null;
            SqlConnection dbConnection = base.CreateConnection();
            SqlCommand command = base.CreateCommand("hpf_pre_purchase_case_detail_get", dbConnection);
            try
            {
                //<Parameter>
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@pi_applicant_id", applicantId);

                //</Parameter>
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        returnObject = new PrePurchaseCaseDTO();
                        #region set PrePurchaseCase value
                        returnObject.PpcId = ConvertToInt(reader["ppc_id"]);
                        returnObject.ApplicantId = ConvertToInt(reader["applicant_id"]);
                        returnObject.ProgramId = ConvertToInt(reader["program_id"]);
                        returnObject.AgencyId = ConvertToInt(reader["agency_id"]);
                        returnObject.AgencyCaseNum = ConvertToString(reader["agency_case_num"]);
                        returnObject.AcctNum = ConvertToString(reader["acct_num"]);
                        returnObject.MortgageProgramCd = ConvertToString(reader["mortgage_program_cd"]);
                        returnObject.BorrowerFName = ConvertToString(reader["borrower_fname"]);
                        returnObject.BorrowerLName = ConvertToString(reader["borrower_lname"]);
                        returnObject.CoBorrowerFName = ConvertToString(reader["co_borrower_fname"]);
                        returnObject.CoBorrowerLName = ConvertToString(reader["co_borrower_lname"]);
                        returnObject.PropAddr1 = ConvertToString(reader["prop_addr1"]);
                        returnObject.PropAddr2 = ConvertToString(reader["prop_addr2"]);
                        returnObject.PropCity = ConvertToString(reader["prop_city"]);
                        returnObject.PropStateCd = ConvertToString(reader["prop_state_cd"]);
                        returnObject.PropZip = ConvertToString(reader["prop_zip"]);
                        returnObject.MailAddr1 = ConvertToString(reader["mail_addr1"]);
                        returnObject.MailAddr2 = ConvertToString(reader["mail_addr2"]);
                        returnObject.MailCity = ConvertToString(reader["mail_city"]);
                        returnObject.MailStateCd = ConvertToString(reader["mail_state_cd"]);
                        returnObject.MailZip = ConvertToString(reader["mail_zip"]);
                        returnObject.BorrowerAuthorizationInd = ConvertToString(reader["borrower_authorization_ind"]);
                        returnObject.MotherMaidenLName = ConvertToString(reader["mother_maiden_lname"]);
                        returnObject.PrimaryContactNo = ConvertToString(reader["primary_contact_no"]);
                        returnObject.SecondaryContactNo = ConvertToString(reader["secondary_contact_no"]);
                        returnObject.BorrowerEmployerName = ConvertToString(reader["borrower_employer_name"]);
                        returnObject.BorrowerJobTitle = ConvertToString(reader["borrower_job_title"]);
                        returnObject.BorrowerSelfEmployedInd = ConvertToString(reader["borrower_self_employed_ind"]);
                        returnObject.BorrowerYearsEmployed = ConvertToDouble(reader["borrower_years_employed"]);
                        returnObject.CoBorrowerEmployerName = ConvertToString(reader["co_borrower_employer_name"]);
                        returnObject.CoBorrowerJobTitle = ConvertToString(reader["co_borrower_job_title"]);
                        returnObject.CoBorrowerSelfEmployedInd = ConvertToString(reader["co_borrower_self_employed_ind"]);
                        returnObject.CoBorrowerYearsEmployed = ConvertToDouble(reader["co_borrower_years_employed"]);
                        returnObject.CounselorIdRef = ConvertToString(reader["counselor_id_ref"]);
                        returnObject.CounselorFName = ConvertToString(reader["counselor_fname"]);
                        returnObject.CounselorLName = ConvertToString(reader["counselor_lname"]);
                        returnObject.CounselorEmail = ConvertToString(reader["counselor_email"]);
                        returnObject.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                        returnObject.CounselorExt = ConvertToString(reader["counselor_ext"]);
                        returnObject.CounselingDurationMins = ConvertToInt(reader["counseling_duration_mins"]).Value;
                        returnObject.CreateAppName = ConvertToString(reader["create_app_name"]);
                        returnObject.CreateDate = ConvertToDateTime(reader["create_dt"]);
                        returnObject.CreateUserId = ConvertToString(reader["create_user_id"]);
                        returnObject.ChangeLastAppName = ConvertToString(reader["chg_lst_app_name"]);
                        returnObject.ChangeLastDate = ConvertToDateTime(reader["chg_lst_dt"]);
                        returnObject.ChangeLastUserId = ConvertToString(reader["chg_lst_user_id"]);
                        returnObject.ChgLstUserId = returnObject.ChangeLastUserId;

                        #endregion
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                command.Dispose();
                dbConnection.Close();
            }
            return returnObject;
        }
    }
}
