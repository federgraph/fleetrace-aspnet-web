namespace FA03.Model
{
    public class CompareDummyClient
    {
    }
}

/*
  // AngularID = 1 
  inputWireConnect(): Observable<string> {
    return this.http.get('/api/input-wire-connect', { responseType: 'text' });
  }

  // AngularID = 2 
  inputWireDisconnect(): Observable<string> {
    return this.http.get('/api/input-wire-disconnect', { responseType: 'text' });
  }

  // AngularID = 3 
  outputWireConnect(): Observable<string> {
    return this.http.get('/api/output-wire-connect', { responseType: 'text' });
  }

  // AngularID = 4 
  outputWireDisconnect(): Observable<string> {
    return this.http.get('/api/output-wire-disconnect', { responseType: 'text' });
  }

  // AngularID = 5 
  queryParams(): Observable<EventParams> {
    return this.http.get<EventParams>('/api/query-params', {});
  }

  // AngularID = 6 
  manageClear(): Observable<string> {
    return this.http.get('/api/manage-clear', { responseType: 'text' });
  }

  // AngularID = 7 
  manageClearRace(race: number): Observable<string> {
    return this.http.get(`/api/manage-clear-race?race=${race}`, { responseType: 'text' });
  }

  // AngularID = 8 
  manageGoBackToRace(race: number): Observable<string> {
    return this.http.get(`/api/manage-go-back-to-race?race=${race}`, { responseType: 'text' });
  }

  // AngularID = 9
  manageClearTimepoint(race: number, it: number): Observable<string> {
    return this.http.get(`/api/manage-clear-timepoint?race=${race}&it=${it}`, { responseType: 'text' });
  }

  // AngularID = 10
  sendTime(race: number, it: number, bib: number): Observable<string> {
    return this.http.get(`/api/widget/time?race=${race}&it=${it}&bib=${bib}`, { responseType: 'text' });
  }

  // AngularID = 11
  sendMsg(value: string): Observable<string> {
    return this.http.get(`/api/send-msg?value=${value}`, { responseType: 'text' });
  }

  // AngularID = 12
  requestNetto(): Observable<string> {
    //get-input-netto
    return this.http.get('/api/widget/netto', {responseType: 'text'});
  }

  // AngularID = 13
  requestOutputNetto(): Observable<string> {
    return this.http.get('/api/widget/get-output-netto', {responseType: 'text'});
  }

  //AngularID = 14
  requestInputNetto(): Observable<string> {
    return this.http.get('/api/widget/get-input-netto', {responseType: 'text'});
  }

  // AngularID = 15
  requestNetto1(): Observable<string> {
    const headers = new HttpHeaders();
    headers.append('Access-Control-Allow-Origin', 'http://localhost:3000');

  return this.http.get('api/widget/netto', {
      "headers": headers,
      "responseType": 'text'
    });
  }

  // AngularID = 16
  pullE(): Observable<string> {
    return this.http.get('/api/event-data', { responseType: 'text' })
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 17
  pullEJ(): Observable<EventDataJson> {
    return this.http.get<EventDataJson>('/api/event-data-json', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 18
  pullRJ(race: number): Observable<RaceDataJson> {
    let p: HttpParams = new HttpParams();
    p = p.set('race', race.toString());

    const o: QueryOptions =  new QueryOptions();
    o.headers = JsonOptions.headers;
    o.params = p;

    return this.http.get<RaceDataJson>('/api/race-data-json', o)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 19
  pushE(value: string): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/api/event-data', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 20
  pushEJ(value: EventDataJson): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/api/event-data-json', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 21
   pushRJ(race: number, value: RaceDataJson): Observable<ApiRetValue> {
    let p: HttpParams = new HttpParams();
    p = p.set('race', race.toString());

    const o: QueryOptions =  new QueryOptions();
    o.headers = JsonOptions.headers;
    o.params = p;

    return this.http.post<ApiRetValue>('/api/race-data-json', value, o)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 22
  pushRD(value: RaceDataJson): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/api/rd.json', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 23
  pushED(value: EventDataJson): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/api/ed.json', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 24
  push2(value: EventDataJson): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/ud/2', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 25
  push3(value: RaceDataJson): Observable<ApiRetValue> {
    return this.http.post<ApiRetValue>('/ud/3', value, JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 26
  pullED(): Observable<EventDataJson> {
    return this.http.get<EventDataJson>('/api/ed.json', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 27
    pullRD(): Observable<RaceDataJson> {
    return this.http.get<RaceDataJson>('/api/rd.json', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 28
  pull2(): Observable<EventDataJson> {
    return this.http.get<EventDataJson>('/ud/2', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 29
  pull3(): Observable<RaceDataJson> {
    return this.http.get<RaceDataJson>('/ud/3', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 30
  getBackup(): Observable<string[]> {
    return this.http.get<string[]>('/api/backup', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 31
  getBacklog(): Observable<string[]> {
    return this.http.get<string[]>('/api/backlog', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 32
  getBackupAndLog(): Observable<string[]> {
    return this.http.get<string[]>('/api/backup-and-log', JsonOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 33
  getBackupString(): Observable<string> {
    return this.http.get('/api/backup-string', { responseType: 'text' })
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 34
  getBacklogString(): Observable<string> {
    return this.http.get('/api/backlog-string', { responseType: 'text' })
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 35
  getBackupAndLogString(): Observable<string> {
    return this.http.get('/api/backup-and-log-string', { responseType: 'text' })
      .pipe(
        catchError(this.handleError)
      );
  }

  // AngularID = 36
  getBackupAndLogJsonString(): Observable<string> {
    return this.http.get('/api/backup-and-log-json-string', { responseType: 'text' })
      .pipe(
        catchError(this.handleError)
      );
  }


  // AngularID = 37
  getEventTableJson(mode: number) : Observable<string> {
    return this.http.get(`/api/widget/get-event-table-json? mode =${mode}`, {responseType: 'text'});
  }

  // AngularID = 38
  getFinishTableJson() : Observable<string> {
    return this.http.get('/api/widget/get-finish-table-json', {responseType: 'text'});
  }

  // AngularID = 39
  getPointsTableJson() : Observable<string> {
    return this.http.get('/api/widget/get-points-table-json', {responseType: 'text'});
  }

  // AngularID = 40
  getNarrowRaceTableJson(race: number, it: number) : Observable<string> {
    return this.http.get(`/api/widget/get-narrow-race-table-json?race=${race}&it=${it}`, {responseType: 'text'});
  }

  // AngularID = 41
  getWideRaceTableJson(race: number, it: number) : Observable<string> {
    return this.http.get(`/api/widget/get-wide-race-table-json?race=${race}&it=${it}`, {responseType: 'text'});
  }

  // AngularID = 42
  getRaceTableJson() : Observable<string> {
    return this.http.get('/api/widget/get-race-table-json', {responseType: 'text'});
  }

  // AngularID = 43
  getRaceTableHtml() : Observable<string> {
    return this.http.get('/api/widget/get-race-table-html', {responseType: 'text'});
  }

  // AngularID = 44
  getTime(race: number, it: number, bib: number) : Observable<string> {
    return this.http.get(`/api/widget/do-time?race=${race}&it=${it}&bib=${bib}`, {responseType: 'text'});
  }

  // AngularID = 45
  getFinish(race: number, bib: number) : Observable<string> {
    return this.http.get(`/api/widget/do-finish?race=${race}&bib=${bib}`, {responseType: 'text'});
  }

  // AngularID = 46
  getTimeAndTable(race: number, it: number, bib: number) : Observable<string> {
    return this.http.get(`/api/widget/do-time-for-table?race=${race}&it=${it}&bib=${bib}`, {responseType: 'text'});
  }

  // AngularID = 47
  getFinishAndTable(race: number, bib: number) : Observable<string> {
    return this.http.get(`/api/widget/do-finish-for-table?race=${race}&bib=${bib}`, {responseType: 'text'});
  }

  // AngularID = 48
  getTimingEventForTable(race: number, it: number, bib: number, option: number, mode: number) : Observable<string> {
    return this.http.get(`/api/widget/do-timing-event-for-table? race =${race}&it=${it}&bib=${bib}&option=${option}&mode=${mode}`,
      {responseType: 'text'});
  }

  // AngularID = 49
  getTimingEvent(race: number, it: number, bib: number, option: number) : Observable<string> {
    return this.http.get(`/api/widget/do-timing-event?race=${race}&it=${it}&bib=${bib}&option=${option}`, {responseType: 'text'});
  }

  // AngularID = 50
  getTimingEventQuick(race: number, it: number, bib: number, option: number) : Observable<string> {
    return this.http.get(`/api/widget/do-timing-event-quick? race =${race}&it=${it}&bib=${bib}`, {responseType: 'text'});
  }

  // AngularID = 51
  getInputConnStatus() : Observable<string> {
    return this.http.get('/api/get-input-connection-status', {responseType: 'text'});
  }

  // AngularID = 52
  getOutputConnStatus() : Observable<string> {
    return this.http.get('/api/get-output-connection-status', {responseType: 'text'});
  }

*/
