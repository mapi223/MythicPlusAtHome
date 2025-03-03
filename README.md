I am getting an error after this line this.http.get<any[]>('https://localhost:7174/group/') 
  .subscribe((GetResponse) => {
    this.RoleAssignments = GetResponse;
  },
