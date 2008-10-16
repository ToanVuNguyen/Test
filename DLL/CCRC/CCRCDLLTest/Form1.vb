Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports CCRC


Imports CCRCEncryption

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Obj As New CCRC.ReferralEmailHelper

        Dim MyConnection As New OleDbConnection


        'MyConnection.ConnectionString = "Data Source=192.168.1.11,1433;Initial Catalog=CCRC;User ID=CCRC_APP_USER;Pwd=Lj7tC28s;"
        MyConnection.ConnectionString = "Provider=sqloledb;Server=192.168.1.11,1433;Database=ccrc;Uid=CCRC_APP_USER;Pwd=Lj7tC28s;"


        MyConnection.Open()

        Dim selectCommand As New OleDbCommand
        selectCommand = MyConnection.CreateCommand()
        selectCommand.CommandText = "Select * from CCRC_user"
        ' 
        'selectCommand.Connection.Open()
        selectCommand.ExecuteReader(CommandBehavior.CloseConnection)


        'EMailText.Text = Obj.CouselingSummaryEmailBody("1448", "a", "1", "1", "jfuhrma")
        'Obj.EmailLog(DateTime.Now, "abc@abc.com", "abc@abc.com", "123", "123", "123", "123", "a", DateTime.Now, "1", DateTime.Now, "1", "1", "1", "1", "1212", "1.pdf", "2.pdf", "3.pdf")
        'Obj.UpdateEmailLog(1, DateTime.Now, "abc@abc.com", "abc@abc.com", "123", "123", "123", "123", "a", DateTime.Now, "1", DateTime.Now, "1", "1", "1", "1", "1212")
        'Obj.UpdateEmail("sam@sam.com", "Sept.18(Bloomberg)--TheBankofCanadajoinedtheFederalReserveandothercentralbankstodayinagreementstoprovideextraU.S.dollarstoprivatelendersifneededtocalmfinancialmarkets.CanadascentralbankagreedtoaswapfacilitywiththeFed,underwhichitcoulduse$10billiontoinjectliquidityintoCanadianmoneymarkets.TheBankofCanadahasntusedanyofthatmoneyyetbecausedomesticlendersarenthavingmuchtroublefindingU.S.dollars,spokesmanJeremyHarrisonsaid.``ThisagreementprovidestheBankofCanadawithadditionalflexibilitytoaddressrapidlyevolvingdevelopmentsinfinancialmarkets,thecentralbanksaidtodayinastatementfromOttawa.``CanadacontinuestocloselymonitorglobalmarketdevelopmentsandremainscommittedtoprovidingliquidityasrequiredtosupportthestabilityoftheCanadianfinancialsystem.CanadiancreditmarketswerelessaffectedthanthoseintheU.S.bylastyearscollapseofthesubprimemortgagemarket,thoughconcernsintensifiedafterLehmanBrothersHoldingsInc.filedforbankruptcy.TodaysswapcomesaspartofaFedpackagethatalmostquadrupledtheamountofdollarscentralba", 1450)
        'Obj.GetEmailLog("9/25/2008", "9/26/2008", "18601,19486,14561,19491,110,18001,19041,108,11,401,1758")






        Dim uName, pwd As String
        uName = "jfuhrma"

        Dim LoginBuisness As LoginBuisness = New LoginBuisness()
        Dim dr As SqlDataReader = LoginBuisness.GetHashedPwd(uName)
        Dim strHashedDbPwd As String = ""

        Dim profileSeqId As String = ""
        Dim profileRoles As String = ""
        Dim entitySeqId As String = ""
        Dim entityTypeCode As String = ""

        Dim entityIsActive As String = ""
        Dim userIsActive As String = ""

        While dr.Read()

            entityIsActive = dr("BUSINESS_ENTITY_ACTIVE_IND").ToString()
            userIsActive = dr("USER_ACTIVE_IND").ToString()
            profileSeqId = dr("CCRC_USER_SEQ_ID").ToString()
            profileRoles = dr("USER_ROLE_STRING").ToString()
            entitySeqId = dr("BSN_ENTITY_SEQ_ID").ToString()
            entityTypeCode = dr("BSN_ENTITY_TYPE_CODE").ToString()
            strHashedDbPwd = dr("USER_PASSWORD").ToString()
        End While

        If strHashedDbPwd.Length > 0 Then
            ' Verify that hashed user-entered password is the same
            ' as the hashed password from the database
            If SaltedHash.ValidatePassword("jfuhrma$123", strHashedDbPwd) Then

                MessageBox.Show("Logged In")


            Else

                MessageBox.Show("Login attempt failed.")

            End If
        Else

            MessageBox.Show("Login attempt failed.")
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim r As New Referral()
        If r.IsUseSecureEmail("hpf@wellsfargo.com, hpo.chase@chase.com, mortgagehelp@citi.com") Then
            MessageBox.Show("Y")
        Else
            MessageBox.Show("N")
        End If

    End Sub
End Class
