import 'package:flutter/material.dart';
import 'package:slbfe_citizenapp/view/changepassword.dart';
import 'package:slbfe_citizenapp/view/contacts.dart';
import 'package:slbfe_citizenapp/view/home.dart';
import 'package:slbfe_citizenapp/view/personaldetails.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: Home(),
    );
  }
}
