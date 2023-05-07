import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.scss']
})
export class ExamsComponent implements OnInit {
  dataTable: DataTable[] = [
    { id: 1, firstName: 'Bella', lastName: 'Chloe', position: 'System Developer', startdate: '2018/03/12', salary: 654765, email: 'b.Chloe@datatables.net' },
    { id: 2, firstName: 'Donna', lastName: 'Bond', position: 'Account Manager', startdate: '2012/02/21', salary: 543654, email: 'd.bond@datatables.net' },
    { id: 3, firstName: 'Harry', lastName: 'Carr', position: 'Technical Manager', startdate: '20011/02/87', salary: 86000, email: 'h.carr@datatables.net' },
    { id: 4, firstName: 'Lucas', lastName: 'Dyer', position: 'Javascript Developer', startdate: '2014/08/23', salary: 456123, email: 'l.dyer@datatables.net' },
    { id: 5, firstName: 'Karen', lastName: 'Hill', position: 'Sales Manager', startdate: '2010/7/14', salary: 432230, email: 'k.hill@datatables.net' },
    { id: 6, firstName: 'Dominic', lastName: 'Hudson', position: 'Sales Assistant', startdate: '2015/10/16', salary: 654300, email: 'd.hudson@datatables.net' },
    { id: 7, firstName: 'Herrod', lastName: 'chanler', position: 'Integration Spciaist', startdate: '2012/0/06', salary: 137500, email: 'h.chandler@datatables.net' },
    { id: 8, firstName: 'Jonathan', lastName: 'Ince', position: 'junior Manager', startdate: '2012/11/23', salary: 345789, email: 'j.ince@datatables.net' },
    { id: 9, firstName: 'Leonard', lastName: 'Ellison', position: 'Junior Javascript Developer', startdate: '2010/03/19', salary: 205500, email: 'l.ellison@datatables.net' },
    { id: 10, firstName: 'Madeleine', lastName: 'Lee', position: 'Software Developer', startdate: '2015/8/23', salary: 456890, email: 'm.lee@datatables.net' },
    { id: 11, firstName: 'Karen', lastName: 'Miller', position: 'Office Director', startdate: '2012/9/25', salary: 87654, email: 'k.miller@datatables.net' },
    { id: 12, firstName: 'Lisa', lastName: 'Smith', position: 'Support Lead', startdate: '2011/05/21', salary: 342000, email: 'l.simth@datatables.net' },
    { id: 13, firstName: 'Morgan', lastName: 'Keith', position: 'Accountant', startdate: '2012/11/27', salary: 675245, email: 'm.keith@datatables.net' },
    { id: 14, firstName: 'Nathan', lastName: 'Mills', position: 'Senior Marketing Designer', startdate: '2014/10/8', salary: 765980, email: 'n.mills@datatables.net' },
    { id: 15, firstName: 'Ruth', lastName: 'May', position: 'office Manager', startdate: '2010/03/17', salary: 654765, email: 'r.may@datatables.net' },
    { id: 16, firstName: 'Penelope', lastName: 'Ogden', position: 'Marketing Manager', startdate: '2013/5/22', salary: 345510, email: 'p.ogden@datatables.net' },
    { id: 17, firstName: 'Sean', lastName: 'Piper', position: 'Financial Officer', startdate: '2014/06/11', salary: 725000, email: 's.piper@datatables.net' },
    { id: 18, firstName: 'Trevor', lastName: 'Ross', position: 'Systems Administrator', startdate: '2011/05/23', salary: 237500, email: 't.ross@datatables.net' },
    { id: 19, firstName: 'Vanessa', lastName: 'Robertson', position: 'Software Designe', startdate: '2014/6/23', salary: 765654, email: 'v.robertson@datatables.net' },
    { id: 20, firstName: 'Una', lastName: 'Richard', position: 'Personnel r', startdate: '2014/5/22', salary: 765290, email: 'u.richard@datatables.net' },
    { id: 21, firstName: 'Justin', lastName: 'Peters', position: 'Development lead', startdate: '2013/10/23', salary: 765654, email: 'j.peters@datatables.net' },
    { id: 22, firstName: 'Adrian', lastName: 'Terry', position: 'Marketing Officer', startdate: '2013/04/21', salary: 543769, email: 'a.terry@datatables.net' },
    { id: 23, firstName: 'Cameron', lastName: 'Watson', position: 'Sales Support', startdate: '2013/9/7', salary: 675876, email: 'c.watson@datatables.net' },
    { id: 24, firstName: 'Evan', lastName: 'Terry', position: 'Sales Manager', startdate: '2013/10/26', salary: 66340, email: 'd.terry@datatables.net' },
    { id: 25, firstName: 'Angelica', lastName: 'Ramos', position: 'Chief Executive Officer', startdate: '20017/10/15', salary: 6234000, email: 'a.ramos@datatables.net' },
    { id: 26, firstName: 'Conno', lastName: 'Johne', position: 'Web Developer', startdate: '2011/1/25', salary: 92575, email: 'C.johne@datatables.net' },
    { id: 27, firstName: 'Jennifer', lastName: 'Chang', position: 'Regional Director', startdate: '2012/17/11', salary: 546890, email: 'j.chang@datatables.net' },
    { id: 28, firstName: 'Brenden', lastName: 'Wagner', position: 'Software Engineer', startdate: '2013/07/14', salary: 206850, email: 'b.wagner@datatables.net' },
    { id: 29, firstName: 'Fiona', lastName: 'Green', position: 'Chief Operating Officer', startdate: '2015/06/23', salary: 345789, email: 'f.green@datatables.net' },
    { id: 30, firstName: 'Shou', lastName: 'Itou', position: 'Regional Marketing', startdate: '2013/07/19', salary: 335300, email: 's.itou@datatables.net' },
    { id: 31, firstName: 'Michelle', lastName: 'House', position: 'Integration Specialist', startdate: '2016/07/18', salary: 76890, email: 'm.house@datatables.net' },
    { id: 32, firstName: 'Suki', lastName: 'Burks', position: 'Developer', startdate: '2010/11/45', salary: 678890, email: 's.burks@datatables.net' },
    { id: 33, firstName: 'Prescott', lastName: 'Bartlet', position: 'Technical Author', startdate: '2014/12/25', salary: 789100, email: 'p.bartlett@datatables.net' },
    { id: 34, firstName: 'Gavin', lastName: 'Cortez', position: 'Team Leader', startdate: '2015/1/19', salary: 345890, email: 'g.cortez@datatables.net' },
    { id: 35, firstName: 'Martena', lastName: 'Mccray', position: 'Post-Sales support', startdate: '2011/03/09', salary: 324050, email: 'm.mccray@datatables.net' },
    { id: 36, firstName: 'Unity', lastName: 'Butler', position: 'Marketing Designer', startdate: '2014/7/28', salary: 34983, email: 'u.butler@datatables.net' },
    { id: 37, firstName: 'Howard', lastName: 'Hatfield', position: 'Office Manager', startdate: '2013/8/19', salary: 98000, email: 'h.hatfield@datatables.net' },
    { id: 38, firstName: 'Hope', lastName: 'Fuentes', position: 'Secretary', startdate: '2015/07/28', salary: 78879, email: 'h.fuentes@datatables.net' },
    { id: 39, firstName: 'Vivian', lastName: 'Harrell', position: 'Financial Controller', startdate: '2010/02/14', salary: 452500, email: 'v.harrell@datatables.net' },
    { id: 40, firstName: 'Timothy', lastName: 'Mooney', position: 'Office Manager', startdate: '2016/12/11', salary: 136200, email: 't.mooney@datatables.net' },
    { id: 41, firstName: 'Jackson', lastName: 'Bradshaw', position: 'Director', startdate: '2011/09/26', salary: 645750, email: 'j.bradshaw@datatables.net' },
    { id: 42, firstName: 'Olivia', lastName: 'Liang', position: 'Support Engineer', startdate: '2014/02/03', salary: 234500, email: 'o.liang@datatables.net' },
    { id: 43, firstName: 'Bruno', lastName: 'Nash', position: 'Software Engineer', startdate: '2015/05/03', salary: 163500, email: 'b.nash@datatables.net' },
    { id: 44, firstName: 'Sakura', lastName: 'Yamamoto', position: 'Support Engineer', startdate: '2010/08/19', salary: 139575, email: 's.yamamoto@datatables.net' },
    { id: 45, firstName: 'Thor', lastName: 'Walton', position: 'Developer', startdate: '2012/08/11', salary: 98540, email: 't.walton@datatables.net' },
    { id: 46, firstName: 'Finn', lastName: 'Camacho', position: 'Support Engineer', startdate: '2016/07/07', salary: 87500, email: 'f.camacho@datatables.net' },
    { id: 47, firstName: 'Serge', lastName: 'Baldwin', position: 'Data Coordinator', startdate: '2017/04/09', salary: 138575, email: 's.baldwin@datatables.net' },
    { id: 48, firstName: 'Zenaida', lastName: 'Frank', position: 'Software Engineer', startdate: '2018/01/04', salary: 125250, email: 'z.frank@datatables.net' },
    { id: 49, firstName: 'Zorita', lastName: 'Serrano', position: 'Software Engineer', startdate: '2017/06/01', salary: 115000, email: 'z.serrano@datatables.net' },
    { id: 50, firstName: 'Jennifer', lastName: 'Acosta', position: 'Junior Javascript Developer', startdate: '2017/02/01', salary: 75650, email: 'j.acosta@datatables.net' },
    { id: 51, firstName: 'Bella', lastName: 'Chloe', position: 'System Developer', startdate: '2018/03/12', salary: 654765, email: 'b.Chloe@datatables.net' },
    { id: 52, firstName: 'Donna', lastName: 'Bond', position: 'Account Manager', startdate: '2012/02/21', salary: 543654, email: 'd.bond@datatables.net' },
    { id: 53, firstName: 'Harry', lastName: 'Carr', position: 'Technical Manager', startdate: '20011/02/87', salary: 86000, email: 'h.carr@datatables.net' },
    { id: 54, firstName: 'Lucas', lastName: 'Dyer', position: 'Javascript Developer', startdate: '2014/08/23', salary: 456123, email: 'l.dyer@datatables.net' },
    { id: 55, firstName: 'Karen', lastName: 'Hill', position: 'Sales Manager', startdate: '2010/7/14', salary: 432230, email: 'k.hill@datatables.net' },
    { id: 56, firstName: 'Dominic', lastName: 'Hudson', position: 'Sales Assistant', startdate: '2015/10/16', salary: 654300, email: 'd.hudson@datatables.net' },
    { id: 57, firstName: 'Herrod', lastName: 'chanler', position: 'Integration Spciaist', startdate: '2012/0/06', salary: 137500, email: 'h.chandler@datatables.net' },
    { id: 58, firstName: 'Jonathan', lastName: 'Ince', position: 'junior Manager', startdate: '2012/11/23', salary: 345789, email: 'j.ince@datatables.net' },
    { id: 59, firstName: 'Leonard', lastName: 'Ellison', position: 'Junior Javascript Developer', startdate: '2010/03/19', salary: 205500, email: 'l.ellison@datatables.net' },
    { id: 60, firstName: 'Madeleine', lastName: 'Lee', position: 'Software Developer', startdate: '2015/8/23', salary: 456890, email: 'm.lee@datatables.net' },
    { id: 61, firstName: 'Karen', lastName: 'Miller', position: 'Office Director', startdate: '2012/9/25', salary: 87654, email: 'k.miller@datatables.net' },
    { id: 62, firstName: 'Lisa', lastName: 'Smith', position: 'Support Lead', startdate: '2011/05/21', salary: 342000, email: 'l.simth@datatables.net' },
    { id: 63, firstName: 'Morgan', lastName: 'Keith', position: 'Accountant', startdate: '2012/11/27', salary: 675245, email: 'm.keith@datatables.net' },
    { id: 64, firstName: 'Nathan', lastName: 'Mills', position: 'Senior Marketing Designer', startdate: '2014/10/8', salary: 765980, email: 'n.mills@datatables.net' },
    { id: 65, firstName: 'Ruth', lastName: 'May', position: 'office Manager', startdate: '2010/03/17', salary: 654765, email: 'r.may@datatables.net' },
    { id: 66, firstName: 'Penelope', lastName: 'Ogden', position: 'Marketing Manager', startdate: '2013/5/22', salary: 345510, email: 'p.ogden@datatables.net' },
    { id: 67, firstName: 'Sean', lastName: 'Piper', position: 'Financial Officer', startdate: '2014/06/11', salary: 725000, email: 's.piper@datatables.net' },
    { id: 68, firstName: 'Trevor', lastName: 'Ross', position: 'Systems Administrator', startdate: '2011/05/23', salary: 237500, email: 't.ross@datatables.net' },
    { id: 69, firstName: 'Vanessa', lastName: 'Robertson', position: 'Software Designe', startdate: '2014/6/23', salary: 765654, email: 'v.robertson@datatables.net' },
    { id: 70, firstName: 'Una', lastName: 'Richard', position: 'Personnel r', startdate: '2014/5/22', salary: 765290, email: 'u.richard@datatables.net' },
    { id: 71, firstName: 'Justin', lastName: 'Peters', position: 'Development lead', startdate: '2013/10/23', salary: 765654, email: 'j.peters@datatables.net' },
    { id: 72, firstName: 'Adrian', lastName: 'Terry', position: 'Marketing Officer', startdate: '2013/04/21', salary: 543769, email: 'a.terry@datatables.net' },
    { id: 73, firstName: 'Cameron', lastName: 'Watson', position: 'Sales Support', startdate: '2013/9/7', salary: 675876, email: 'c.watson@datatables.net' },
    { id: 74, firstName: 'Evan', lastName: 'Terry', position: 'Sales Manager', startdate: '2013/10/26', salary: 66340, email: 'd.terry@datatables.net' },
    { id: 75, firstName: 'Angelica', lastName: 'Ramos', position: 'Chief Executive Officer', startdate: '20017/10/15', salary: 6234000, email: 'a.ramos@datatables.net' },
    { id: 76, firstName: 'Conno', lastName: 'Johne', position: 'Web Developer', startdate: '2011/1/25', salary: 92575, email: 'C.johne@datatables.net' },
    { id: 77, firstName: 'Jennifer', lastName: 'Chang', position: 'Regional Director', startdate: '2012/17/11', salary: 546890, email: 'j.chang@datatables.net' },
    { id: 78, firstName: 'Brenden', lastName: 'Wagner', position: 'Software Engineer', startdate: '2013/07/14', salary: 206850, email: 'b.wagner@datatables.net' },
    { id: 79, firstName: 'Fiona', lastName: 'Green', position: 'Chief Operating Officer', startdate: '2015/06/23', salary: 345789, email: 'f.green@datatables.net' },
    { id: 80, firstName: 'Shou', lastName: 'Itou', position: 'Regional Marketing', startdate: '2013/07/19', salary: 335300, email: 's.itou@datatables.net' },

  ]
  constructor() { }

  ngOnInit() {
  }

}

export class DataTable {
  id?: number;
  firstName?: string;
  lastName?: string;
  position?: string;
  startdate?: string;
  salary?: number;
  email?: string;
}